var movieController = function(){
    function all(context){
        var movies;
        jsonRequester.get('http://localhost:52189/api/Movies')
            .then(function (resp) {
                movies = resp;
                return templates.get('movies')
            }).then(function(template){
                console.log(movies);
                context.$element().html(template(movies));
            });
    }

    return {
        all: all
    };
}();
