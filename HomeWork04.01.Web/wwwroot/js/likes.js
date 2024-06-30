$(() => {
    setInterval(() => {
        updateLikes();
    }, 1000);

    $("#like-button").on('click', function () {
        const id = $("#question-id").val();
        $.post('/home/likeimage', { id }, function () {
            updateLikes();
            $("#like-button").prop('disabled', true);
        });
    });

    function updateLikes() {
        const id = $("#question-id").val();
        $.get(`/home/getlikes`, { id }, function ({ likes }) {
            $("#likes-count").text(likes);
        });
    }
});