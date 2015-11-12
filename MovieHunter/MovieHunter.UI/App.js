var app = Sammy('#content', function () {

    this.get('#/', function () {
         this.redirect('#/home');
    });

    this.get('#/home', homeController.all);
    this.get('#/movies', movieController.all);
});

app.run('#/');