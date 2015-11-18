var userController = function() {

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
                            toastr.success('Hurry up, Start hunting', 'Welcome');
                        });
                }
            })
        })
    }

    return {
        register: register
    };
}();

