var homeController = function(){
function all(context){
    jsonRequester.get('http://localhost:52189/api/Trailers')
            .then(function (resp) {
                $('#content').text(JSON.stringify(resp));
            });


           return templates.get('home')
        // }).then(function(template){
        //     context.$element().html(template());
        // });
}

return {
    all: all
};
}();


