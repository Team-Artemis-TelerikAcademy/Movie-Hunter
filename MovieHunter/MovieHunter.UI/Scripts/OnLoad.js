$(document).ready(function(){
    if(!(localStorage.getItem("tokenKey"))){
        $('#my-movies').css('display','none');
        $('#login').css('display','block');
        $('#logout').css('display','none');
    }else{
        $('#my-movies').css('display','block');
        $('#login').css('display','none');
        $('#logout').css('display','block');
    }
    $('#btn-login').on('click', function(){
        event.preventDefault();

        var loginData = {
            grant_type: 'password',
            username: $('#tb-login-username').val(),
            password: $('#tb-login-password').val()
        };

        jsonRequester.post('http://localhost:52189/api/account/token', { data: loginData, contentType: 'application/x-www-form-urlencoded; charset=utf-8' })
            .then(function(data){
                localStorage.setItem("tokenKey", data.access_token);
                $('#my-movies').css('display','block');
                $('#login').css('display','none');
                $('#logout').css('display','block');
                toastr.success('Enjoy our movies', 'Sucess, GO HUNT');
                window.location.href ='#/';
            });
    });

    $('#logout').on('click', function(){
        $('#my-movies').css('display','none');
        $('#login').css('display','block');
        $('#logout').css('display','none');
        toastr.success('Whay are you leaving, come back', 'Bye, Bye');
        localStorage.clear();
    });


    function sdf_FTS(_number,_decimal,_separator)
    {
        var decimal=(typeof(_decimal)!='undefined')?_decimal:2;
        var separator=(typeof(_separator)!='undefined')?_separator:'';
        var r=parseFloat(_number)
        var exp10=Math.pow(10,decimal);
        r=Math.round(r*exp10)/exp10;
        rr=Number(r).toFixed(decimal).toString().split('.');
        b=rr[0].replace(/(\d{1,3}(?=(\d{3})+(?:\.\d|\b)))/g,"\$1"+separator);
        r=(rr[1]?b+'.'+rr[1]:b);

        return r;
    }

    setTimeout(function(){
        $('#counter').text('0');
        $('#counter1').text('0');
        $('#counter2').text('0');
        setInterval(function(){

            var curval=parseInt($('#counter').text());
            var curval1=parseInt($('#counter1').text().replace(' ',''));
            var curval2=parseInt($('#counter2').text());
            if(curval<=707){
                $('#counter').text(curval+1);
            }
            if(curval1<=12280){
                $('#counter1').text(sdf_FTS((curval1+20),0,' '));
            }
            if(curval2<=245){
                $('#counter2').text(curval2+1);
            }
        }, 2);

    }, 500);
});
