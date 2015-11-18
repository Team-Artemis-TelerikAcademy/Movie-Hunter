var myMoviesController = function(){
    function allMyMovies(context){
        var authorization = "Bearer " + localStorage.getItem("tokenKey");
        var movies;
        jsonRequester.get('http://localhost:52189/api/my-movies',{ headers: { Authorization: authorization } })

    .then(function (resp) {
                movies = resp;
                return templates.get('my-movies');
            }).then(function(template){
                context.$element().html(template(movies));
            });
    }

    function wantToWatch(context){
        var authorization = "Bearer " + localStorage.getItem("tokenKey");
        var movies;
        jsonRequester.get('http://localhost:52189/api/my-movies/want-to-watch',{ headers: { Authorization: authorization } })

            .then(function (resp) {
                movies = resp;
                return templates.get('my-movies');
            }).then(function(template){
                context.$element().html(template(movies));
            });
    }

    function watched(context){
        var authorization = "Bearer " + localStorage.getItem("tokenKey");
        var movies;
        jsonRequester.get('http://localhost:52189/api/my-movies/watched',{ headers: { Authorization: authorization } })

            .then(function (resp) {
                movies = resp;
                return templates.get('my-movies');
            }).then(function(template){
                context.$element().html(template(movies));
            });

    }

    return {
        allMyMovies: allMyMovies,
        wantToWatch: wantToWatch,
        watched: watched
    };
}();

