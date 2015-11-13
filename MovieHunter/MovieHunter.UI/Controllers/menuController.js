(function(){
    $('#menu .movies-menu p').on('click', function () {
        var $target = $(event.target);
        $target.parent().siblings().removeClass('active');
        $target.parent().addClass('active');
        $('#menu .movies-menu ul').css('display', 'block');
        $('#menu .movies-menu ul .movie-children').css('display', 'block');
        $('#menu .movies-menu ul .movie-children').css('padding', '0');
        $('#menu .movies-menu ul .movie-children a').css('margin', '0');
        $('#menu .movies-menu ul .movie-children a').css('padding', '0');
    });

    $('#menu .home a').on('click', function () {
        var $target = $(event.target);
        $target.parent().siblings().removeClass('active');
        $target.parent().addClass('active');
        $('#menu .movies-menu ul .movie-children').css('display', 'none')
    });
}())

