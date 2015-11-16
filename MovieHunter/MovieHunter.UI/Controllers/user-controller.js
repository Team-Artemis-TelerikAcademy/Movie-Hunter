var userController = function() {

    function login(context) {
        // event.preventDefault();
        templates.get('login')
            .then(function(template){
                context.$element().html(template());
                $('#btn-go-to-register-button').on('click', function(){
                    context.redirect('#/users/register');
                })

                $('#btn-login').on('click', function(){
                    event.preventDefault();
                    var userData = {
                        username: $('#tb-login-username').val(),
                        password: $('#tb-login-password').val()
                    };
                    data.users.login(userData)
                        .then(function(user){
                            context.redirect('#/');
                            document.location.reload(true);
                        });
                })
            })
    }

    function register(context) {

        templates.get('register').then(function(template){
                context.$element().html(template());
            $('#btn-already-registered').on('click', function(){
                context.redirect('#/users/login');
            });

            $('#btn-register').on('click', function() {
                var userData = {
                    Email: $('#tb-register-email').val(),
                    Password: $('#tb-register-password-initial').val(),
                    ConfirmPassword: $('#tb-register-password-confirmed').val(),
                    Username: $('#tb-register-username').val()
                };

                if (userData.Password ===  $('#tb-register-password-confirmed').val()){
                    event.preventDefault();

                    jsonRequester.post('http://localhost:52189/api/Account/Register', userData)
                        .then(function(resp){
                            console.log("here " + resp)
                            context.redirect('#/');
                            document.location.reload(true);
                        });
                }
            })
        })

    }

    return {
        login: login,
        register: register
    };
}();

