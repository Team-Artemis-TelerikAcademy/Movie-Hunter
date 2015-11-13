var app = Sammy('#content', function () {

    this.get('#/', function () {
         this.redirect('#/home');
    });

    this.get('#/home', homeController.all);
    this.get('#/movies', movieController.all);
    this.get('#/movies:id', movieController.getById);
    this.get('#/movies/released', movieController.released);
    this.get('#/movies/coming-soon', movieController.comingSoon);
});

app.run('#/');