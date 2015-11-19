var myMoviesController = function(){
    function allMyMovies(context){
        var authorization = "Bearer " + localStorage.getItem("tokenKey");
        var movies;
        jsonRequester.get('http://moviehunterproject.azurewebsites.net/api/my-movies',{ headers: { Authorization: authorization } })

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
        jsonRequester.get('http://moviehunterproject.azurewebsites.net/api/my-movies/want-to-watch',{ headers: { Authorization: authorization } })

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
        jsonRequester.get('http://moviehunterproject.azurewebsites.net/api/my-movies/watched',{ headers: { Authorization: authorization } })

            .then(function (resp) {
                movies = resp;
                return templates.get('my-movies');
            }).then(function(template){
                context.$element().html(template(movies));
            });

    }

    function getById(context) {
        console.log(this.params.id);
        var id = this.params.id.substr(1);
        var movie;
        var movieState;
        jsonRequester.get('http://moviehunterproject.azurewebsites.net/api/Movies/' + id)
            .then(function (res) {
                movie = res;
                movieState = movie.State[0];
                return templates.get('my-movies-watched');
            }).then(function (template) {

                context.$element().html(template(movie));
                if(movieState === 0){
                    $("#btn-move").html('Move to watched');
                }else{
                    $("#btn-move").html('Move to wanted');
                }
                $('#btn-remove').on('click', function () {
                        var movieToDelete =
                        {
                            movieId: movie.Id
                        };
                    var movieToDeleteStringified = JSON.stringify(movieToDelete);
                    var authorization = "Bearer " + localStorage.getItem("tokenKey");

                    jsonRequester.del('http://moviehunterproject.azurewebsites.net/api/my-movies/' + movieToDelete.movieId, { data: movieToDeleteStringified, contentType: 'application/json', headers: { Authorization: authorization } })
                        .then(function () {
                            toastr.info('You can add this movie from the menu again', 'Movie Deleted');
                            window.location.href ='#/my-movies';
                        })
                });

                $('#btn-move').on('click', function () {
                    if(movie.State[0] == 1){
                        var likedMovie =
                        {
                            movieId: movie.Id,
                            state: 0
                        };
                    }else{
                        var likedMovie =
                        {
                            movieId: movie.Id,
                            state: 1
                        };
                    }
                    var likedMovieStringified = JSON.stringify(likedMovie);
                    var authorization = "Bearer " + localStorage.getItem("tokenKey");

                    jsonRequester.put('http://moviehunterproject.azurewebsites.net/api/my-movies', { data: likedMovieStringified, contentType: 'application/json', headers: { Authorization: authorization } })
                        .then(function () {

                            toastr.info('Movie State Changed', 'Movie moved successfully');
                            window.location.href ='#/my-movies';
                        })
                });

                var links = $('.actor-link').get();

                $('.actor-link').each(function () {
                    var $attr = $(this).attr('href');
                    var editedAttribute = '';
                    for (i = 0; i < $attr.length; i++) {
                        if ($attr[i] !== ' ') {
                            editedAttribute = editedAttribute + $attr[i]
                        } else {
                            editedAttribute = editedAttribute + '%20'
                        }
                    }

                    $(this).attr('href', editedAttribute);
                })
            })
    }

    return {
        allMyMovies: allMyMovies,
        wantToWatch: wantToWatch,
        watched: watched,
        getById: getById
    };
}();

