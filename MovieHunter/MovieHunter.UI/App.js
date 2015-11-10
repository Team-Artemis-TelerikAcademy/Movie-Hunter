// (function () {
// 
//     var sammyApp = Sammy('#content', function () {
//         this.get('#/', function () {
//             this.redirect('#/home');
//         });
// 
//         this.get('#/home', homeController.all);
//     });
// 
//     $(function () {
//         sammyApp.run('#/');
//     });
// }());


var app = Sammy('#content', function () {

    this.get('#/', function () {
         this.redirect('#/home');
    });

    this.get('#/home', homeController.all);
});

app.run('#/');