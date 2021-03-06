﻿var jsonRequester = (function () {

    function send(method, url, options) {
        options = options || {};

        var header = options.headers || {},
            data = options.data || undefined,
            contentType = options.contentType;
            //authorization = options.authorization || undefined

        // console.log("here " + data);
        // console.log("method " + method);
        // console.log("url " + url);
        // console.log("contentType " + contentType);
        // console.log("authorization " + header);
        var promise = new Promise(function (resolve, reject) {
            $.ajax({
                url: url,
                method: method,
                contentType: contentType,
                crossDomain: true,
                headers: header,
                //Authorization: authorization,
                data: data,
                success: function (res) {
                    resolve(res);
                },
                error: function (err) {
                    reject(err);
                }
            });
        });
        return promise;
    }

    function get(url, options) {
        return send('GET', url, options);
    }

    function post(url, options) {
        return send('POST', url, options);
    }

    function put(url, options) {
        return send('PUT', url, options);
    }

    function del(url, options) {
        return send('DELETE', url, options);
    }

    return {
        send: send,
        get: get,
        post: post,
        put: put,
        del: del
    };
}());