var homeController = function(){
function all(context){
    var trailers;
    jsonRequester.get('http://localhost:52189/api/Trailers')
            .then(function (resp) {
            //     $('#content').text(JSON.stringify(resp));
            // });

            trailers = resp;
            return templates.get('home')
         }).then(function(template){
            // console.log(template);
            console.log(trailers);
            context.$element().html(template(trailers));
});
}

return {
    all: all
};
}();


