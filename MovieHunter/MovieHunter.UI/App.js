var app = Sammy('#content', function () {

    this.get('#/', function () {
         this.redirect('#/home');
    });

    this.get('#/home', homeController.all);

    this.get('#/register', userController.register);
    this.get('#/login', userController.login);

    this.get('#/movies', movieController.all);
    this.get('#/movies:id', movieController.getById);
    this.get('#/movies/released', movieController.released);
    this.get('#/movies/coming-soon', movieController.comingSoon);
    this.get('#/movies/genre/:genre', movieController.getMoviesByGenre);

    this.get('#/my-movies', myMoviesController.allMyMovies);
    this.get('#/my-movies/want-to-watch', myMoviesController.wantToWatch);
    this.get('#/my-movies/watched', myMoviesController.watched);
    this.get('#/my-movies:id', myMoviesController.getById);

    this.get('#/genres', genreController.all);
    this.get('#/my-movies:id', movieController.getById);

    this.get('#/actors/name/:name', actorsController.getActorByName);
});

app.run('#/');