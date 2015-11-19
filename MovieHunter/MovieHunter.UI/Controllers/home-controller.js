var homeController = function(){
function all(context){
    var trailers;
    jsonRequester.get('http://moviehunterproject.azurewebsites.net/api/Trailers')
            .then(function (resp) {
            trailers = resp;
            return templates.get('home')
         }).then(function(template){
            context.$element().html(template(trailers));
        });
}

return {
    all: all
};
}();


