var actorsController = function(){
    function getActorByName(context){
        var actor;
        console.log(this.params);
        jsonRequester.get('http://localhost:52189/api/actors?name=' + this.params.name)
            .then(function (resp) {
                actor = resp;
                console.log(actor)
                return templates.get('actor')
            }).then(function(template){
                context.$element().html(template(actor));
            });
    }

    return {
        getActorByName: getActorByName
    };
}();
