var actorsController = function(){
    function getActorByName(context){
        var actor;
        var actorName = this.params.name;
        var editedActorName = '';
        for(var i = 1; i < actorName.length; i++){
            if(actorName[i]!== ' ') {
                editedActorName = editedActorName + actorName[i]
            } else{
                editedActorName = editedActorName + '%20'
            }
        }
        console.log(editedActorName)
        jsonRequester.get('http://localhost:52189/api/actors?name=' + editedActorName)
            .then(function (resp) {
                actor = resp;
                console.log(actor.name)
                return templates.get('actor')
            }).then(function(template){
                context.$element().html(template(actor));
            });
    }

    return {
        getActorByName: getActorByName
    };
}();
