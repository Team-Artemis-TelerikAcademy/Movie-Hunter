var movieController = function(){
    function all(context){
        var movies;
        jsonRequester.get('http://localhost:52189/api/Movies')
            .then(function (resp) {
                movies = resp;
                return templates.get('movies');
            }).then(function(template){
                console.log(movies);
                context.$element().html(template(movies));
            });
    }

    function getById(context){
        console.log(context);
        var movie;
        jsonRequester.get('http://localhost:52189/api/Movies'+this.params.id)
             .then(function(res) {
                console.log(res);
                movie = res;
                 return templates.get('moviesById');
            }).then(function(template){
                console.log(movie);
                context.$element().html(template(movie));
             })
    }

    function released(context){
        var movies;
        jsonRequester.get('http://localhost:52189/api/movies/released')
            .then(function (resp) {
                console.log('resp ' + resp);
                movies = resp;
                return templates.get('movies');
            }).then(function(template){
                console.log(movies);
                context.$element().html(template(movies));
            });
    }

    function comingSoon(context){
        var movies;
        jsonRequester.get('http://localhost:52189/api/movies/comming-soon')
            .then(function (resp) {
                movies = resp;
                return templates.get('movies');
            }).then(function(template){
                console.log(movies);
                context.$element().html(template(movies));
            });
    }

    return {
        all: all,
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