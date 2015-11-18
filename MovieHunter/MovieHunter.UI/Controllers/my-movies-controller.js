var myMoviesController = function(){
    function allMyMovies(context){
        var movies;
        jsonRequester.get('http://localhost:52189/api/Movies')
            .then(function (resp) {
                movies = resp;
                return templates.get('my-movies');
            }).then(function(template){
                context.$element().html(template(movies));
            });
    }

    function wantToWatch(){

    }

    function getMyMovieById(){



    }

    function watched(){

    }

    return {
        allMyMovies: allMyMovies,
        addMovieToMyMovies: addMovieToMyMovies,
        wantToWatch:wantToWatch,
        getMyMovieById: getMyMovieById,
        watched: watched
    };
}();

