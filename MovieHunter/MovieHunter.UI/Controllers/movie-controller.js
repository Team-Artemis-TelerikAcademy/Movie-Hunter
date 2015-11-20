var movieController = function () {
    function all(context) {
        var movies;
        var page = 1;
        jsonRequester.get('http://moviehunterproject.azurewebsites.net/api/Movies')
            .then(function (resp) {
                movies = resp;
                return templates.get('movies');
            }).then(function (template) {
                context.$element().html(template(movies));
                $("#previous").on('click', function () {
                    if (page > 1) {
                        page = page - 1;
                    }
                    else {
                        page = 1;
                    }
                });
                $("#next").on('click', function () {
                    if (page < 12) {
                        page = page + 1;
                    }
                    else {
                        page = 11;
                    }
                });
                jsonRequester.get('http://moviehunterproject.azurewebsites.net/api/Movies?page=' + page)
                    .then(function (resp) {
                        movies = resp;
                        return templates.get('movies');
                    }).then(function (template) {
                        context.$element().html(template(movies));
                    });
            });
    }

    function getById(context) {
        console.log(this.params.id);
        var id = this.params.id.substr(1);
        var movie;
        jsonRequester.get('http://moviehunterproject.azurewebsites.net/api/Movies/' + id)
             .then(function (res) {
                movie = res;
                console.log(movie);
                 return templates.get('movieById');
             }).then(function (template) {
                 context.$element().html(template(movie));
                if(!(localStorage.getItem("tokenKey"))){
                    $('#btn-add-to-watched').css('display', 'none');
                    $('#btn-add-to-want-to-watch').css('display', 'none');
                    $('.change-rating').css('display', 'none');
                }else {
                    $('#btn-add-to-watched').css('display', 'inline-block');
                    $('#btn-add-to-want-to-watch').css('display', 'inline-block');
                    $('.change-rating').css('display', 'inline-block');
                }
                 $('#btn-add-to-want-to-watch').on('click', function () {
                     console.log(movie);
                     var likedMovie =
                     {
                         movieId: movie.Id,
                         state: 0
                     };

                     var likedMovieStringified = JSON.stringify(likedMovie);
                     var authorization = "Bearer " + localStorage.getItem("tokenKey");

                     jsonRequester.post('http://moviehunterproject.azurewebsites.net/api/my-movies', { data: likedMovieStringified, contentType: 'application/json', headers: { Authorization: authorization } })
                         .then(function () {

                             toastr.success('Go to my movies', 'Movie added successfully');
                         })
                 });

                $('#btn-add-to-watched').on('click', function () {
                    console.log(movie);
                    var likedMovie =
                    {
                        movieId: movie.Id,
                        state: 1
                    };

                    var likedMovieStringified = JSON.stringify(likedMovie);
                    var authorization = "Bearer " + localStorage.getItem("tokenKey");

                    jsonRequester.post('http://moviehunterproject.azurewebsites.net/api/my-movies', { data: likedMovieStringified, contentType: 'application/json', headers: { Authorization: authorization } })
                        .then(function () {
                            toastr.success('Go to my movies', 'Movie added successfully');
                        })
                });

                $('#url-button').on('click', function () {
                    var likedMovie =
                    {
                        Id: movie.Id,
                        state: 0
                    };

                    jsonRequester.get('http://moviehunterproject.azurewebsites.net/api/movies/' + likedMovie.Id + '/download-wallpaper', { contentType: 'application/json'})
                         .then(function (resp) {
                             url = resp;
                             console.log(url)
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



                $('#chanceSlider').on('change', function(){
                    $('#chance').val($('#chanceSlider').val());
                });

                $('#chance').on('keyup', function(){
                    $('#chanceSlider').val($('#chance').val());
                });

                $('.change-rating').on('click', function(){
                    $('.slider-form').css('display', 'inline-block');
                    $('.change-rating').css('display', 'none')
                })


                $('.submit-new-rating').on('click', function(){
                    var newMovieRatingModel =
                    {
                        Id: movie.Id,
                        Rating: $('#chance').val()
                    };

                    var newMovieRatingModelStringified = JSON.stringify(newMovieRatingModel);
                    var authorization = "Bearer " + localStorage.getItem("tokenKey");

                    jsonRequester.put('http://moviehunterproject.azurewebsites.net/api/movies/' + newMovieRatingModel.Id + '/rating', { data: newMovieRatingModelStringified, contentType: 'application/json', headers: { Authorization: authorization } })
                        .then(function () {
                            toastr.success('Rating Successfully Changed');
                            $('.slider-form').css('display','none');
                            $('.change-rating').css('display', 'inline-block');
                        })
                })


             })
    }

    function released(context) {
        var movies;
        jsonRequester.get('http://moviehunterproject.azurewebsites.net/api/movies/released')
            .then(function (resp) {
                movies = resp;
                return templates.get('movies');
            }).then(function (template) {
                context.$element().html(template(movies));
            });
    }

    function comingSoon(context) {
        var movies;
        jsonRequester.get('http://moviehunterproject.azurewebsites.net/api/movies/comming-soon')
            .then(function (resp) {
                movies = resp;
                return templates.get('movies');
            }).then(function (template) {
                context.$element().html(template(movies));
            });
    }

    function getMoviesByGenre(context) {
        var movies;
        var genre = this.params.genre.substr(1);
        jsonRequester.get('http://moviehunterproject.azurewebsites.net/api/Movies?genre=' + genre)
            .then(function (resp) {
                movies = resp;
                return templates.get('movies')
            }).then(function (template) {
                context.$element().html(template(movies));
            });
    }

    return {
        all: all,
        getMoviesByGenre: getMoviesByGenre,
        getById: getById,
        released: released,
        comingSoon: comingSoon
    };
}();
