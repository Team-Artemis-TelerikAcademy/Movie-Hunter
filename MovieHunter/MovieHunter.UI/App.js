var app = Sammy('#content', function () {

    this.get('#/', function () {
         this.redirect('#/home');
    });

    this.get('#/home', homeController.all);

    this.get('#/register', userController.register);
    this.get('#/login', userController.register);

    this.get('#/movies', movieController.all);
    this.get('#/movies:id', movieController.getById);
    this.get('#/movies/released', movieController.released);
    this.get('#/movies/coming-soon', movieController.comingSoon);
    this.get('#/movies/genre/:genre', movieController.getMoviesByGenre);

    this.get('#/genres', genreController.all);

    this.get('#/actors/name/:name', actorsController.getActorByName);
});

app.run('#/');