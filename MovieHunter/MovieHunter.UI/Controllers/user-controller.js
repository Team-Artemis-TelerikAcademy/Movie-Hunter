var userController = function() {

    function login(context) {
        // event.preventDefault();
        templates.get('login')
            .then(function(template){
                context.$element().html(template());
                $('#btn-go-to-register-button').on('click', function(){
                    context.redirect('#/register');
                })

                $('#btn-login').on('click', function(){
                    event.preventDefault();

                    var loginData = {
                        grant_type: 'password',
                        username: $('#tb-login-username').val(),
                        password: $('#tb-login-password').val()
                    };
                    var encodedData = "username=" + $('#tb-login-username').val() + "&password=" + $('#tb-login-password').val() + "&grant_type=password";

                    jsonRequester.post('http://localhost:52189/api/account/token', { data: encodedData, contentType: 'application/x-www-form-urlencoded; charset=utf-8' })
                        .then(function(data){
                            localStorage.setItem(tokenKey, data.access_token);
                            $('.register-link').css('display','none');
                            $('.login-link').css('display','none');
                            $('.logout-link').css('display','inline-block');
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
                context.redirect('#/login');
            });

            $('#btn-register').on('click', function() {
                var email =  $('#tb-register-email').val();
                var password =  $('#tb-register-password-initial').val();
                var confirmPassword = $('#tb-register-password-confirmed').val();
                var username = $('#tb-register-username').val()

                var userData = {
                    Email:email,
                    Password: password,
                    ConfirmPassword: confirmPassword,
                    UserName: username };

                var headers = { "Access-Control-Allow-Origin": '*'}

                if (userData.Password ===  $('#tb-register-password-confirmed').val()){
                   event.preventDefault();

                    var dataStringified = JSON.stringify(userData);

                    jsonRequester.post('http://localhost:52189/api/account/register', { data: dataStringified, contentType: 'application/json' })
                        .then(function(resp){
                            console.log("here " + resp)
                            document.location.reload(true);
                            context.redirect('#/');

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

