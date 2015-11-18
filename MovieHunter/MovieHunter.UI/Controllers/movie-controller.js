var movieController = function () {
    function all(context) {
        var movies;
        jsonRequester.get('http://localhost:52189/api/Movies')
            .then(function (resp) {
                movies = resp;
                return templates.get('movies');
            }).then(function (template) {
                context.$element().html(template(movies));
            });
    }

    function getById(context) {
        console.log(this.params.id);
        var id = this.params.id.substr(1);
        var movie;
        jsonRequester.get('http://localhost:52189/api/Movies/' + id)
             .then(function (res) {
                 movie = res;
                 return templates.get('movieById');
             }).then(function (template) {
                 context.$element().html(template(movie));
                 $('.btn-add-to-my-movies').on('click', function () {
                     console.log(movie);
                     var likedMovie =
                     {
                         movieId: movie.Id,
                         state: 1
                     }

                     var likedMovieStringified = JSON.stringify(likedMovie);
                     var authorization = "Bearer " + localStorage.getItem("tokenKey");

                     jsonRequester.post('http://localhost:52189/api/my-movies', { data: likedMovieStringified, contentType: 'application/json', headers: { Authorization: authorization } })
                         .then(function () {

                             toastr.success('Go to my movies', 'Movie added successfully');
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

    function released(context) {
        var movies;
        jsonRequester.get('http://localhost:52189/api/movies/released')
            .then(function (resp) {
                movies = resp;
                return templates.get('movies');
            }).then(function (template) {
                context.$element().html(template(movies));
            });
    }

    function comingSoon(context) {
        var movies;
        jsonRequester.get('http://localhost:52189/api/movies/comming-soon')
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
        console.log(this.params.genre);
        jsonRequester.get('http://localhost:52189/api/Movies?genre=' + genre)
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





    //         jsonRequester.get('http://localhost:52189/api/Movies:'+this.params.id)
    //             .then(function(res) {
    //                 item = res.result;
    //                 return templates.get('item-details');
    //             })
    //             .then(function(html) {
    //                 var template = handlebars.compile(html);
    //                 $('#content').html(template(item));
    //             });
    //     });
    //     return {
    //         all: all
    //     };
}();
//