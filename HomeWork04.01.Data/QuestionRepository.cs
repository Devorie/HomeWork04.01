using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore.Internal;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;

namespace HomeWork04._01.Data
{
    public class QuestionsRepository
    {
        private string _connectionString;

        public QuestionsRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        private Tag GetTag(string name)
        {
            using var ctx = new QuestionContext(_connectionString);
            return ctx.Tags.FirstOrDefault(t => t.Name == name);
        }

        private int AddTag(string name)
        {
            using var ctx = new QuestionContext(_connectionString);
            var tag = new Tag { Name = name };
            ctx.Tags.Add(tag);
            ctx.SaveChanges();
            return tag.Id;
        }

        public List<Question> GetQuestionsForTag(string name)
        {
            using var ctx = new QuestionContext(_connectionString);
            return ctx.Questions
                    .Where(c => c.QuestionTags.Any(t => t.Tag.Name == name))
                    .Include(q => q.QuestionTags)
                    .ThenInclude(qt => qt.Tag)
                    .ToList();
        }

        public void AddQuestion(Question question, List<string> tags)
        {
            using var ctx = new QuestionContext(_connectionString);
            ctx.Questions.Add(question);
            ctx.SaveChanges();
            foreach (string tag in tags)
            {
                Tag t = GetTag(tag);
                int tagId;
                if (t == null)
                {
                    tagId = AddTag(tag);
                }
                else
                {
                    tagId = t.Id;
                }
                ctx.QuestionsTags.Add(new QuestionTags
                {
                    QuestionId = question.Id,
                    TagId = tagId
                });
            }

            ctx.SaveChanges();
        }

        public List<Question> GetAllQuestions()
        {
            using var ctx = new QuestionContext(_connectionString);
            return ctx.Questions.
                Include(c => c.Answers).
                Include(q => q.QuestionTags).
                ThenInclude(qt => qt.Tag).ToList();

        }

        public Question GetQuestionById (int Id)
        {
            using var ctx = new QuestionContext(_connectionString);
            return ctx.Questions.
                Include(c => c.Answers).
                Include(q => q.QuestionTags).
                ThenInclude(qt => qt.Tag).FirstOrDefault(c => c.Id == Id);
        }

        public void AddAnswer(Answer answer)
        {
            using var ctx = new QuestionContext(_connectionString);
            ctx.Answers.Add(answer);
            ctx.SaveChanges();
        }

        public void AddLike(int id)
        {
            using var ctx = new QuestionContext(_connectionString);
            ctx.Database.ExecuteSqlInterpolated($"UPDATE Questions SET Likes = Likes + 1 WHERE Id = {id}");
        }

        public int GetLikes(int id)
        {
            using var ctx = new QuestionContext(_connectionString);
            return ctx.Questions.Where(i => i.Id == id).Select(i => i.Likes).FirstOrDefault();
        }
    }
}
