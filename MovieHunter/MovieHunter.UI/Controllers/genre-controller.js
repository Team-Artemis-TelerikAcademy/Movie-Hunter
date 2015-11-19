var genreController = function(){
    function all(context){
        var genres;
        jsonRequester.get('http://localhost:52189/api/Genres')
            .then(function (resp) {
                genres = resp;
                console.log(genres.length);
                return templates.get('genres')
           }).then(function(template){
                context.$element().html(template(genres));
            });
    }

    return {
        all: all
    };
}();

