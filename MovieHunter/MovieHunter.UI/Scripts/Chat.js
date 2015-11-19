var CHBT_SERVER = 'https://www.chatbutton.com';
var CHBT_DebugMode = 0;
var CHBT_channel;
var CHBT_title;
var CHBT_titlecolor;
var CHBT_size;
var CHBT_bgcolor;
var CHBT_bordercolor;
var CHBT_textsize;
var CHBT_textcolor;
var CHBT_nncolor;
var CHBT_usercolor;
var CHBT_cpcolor;
var CHBT_flashcolor;
var CHBT_profanityfilter;
var CHBT_loadmessages;
var CHBT_QS;
var CHBT_position;
var CHBT_roomurl;
var CHBT_SID = Math.random();
if (!CHBT_size) {
    CHBT_size = "400x280";
}
var CHBT_sizes = CHBT_size.split("x");
var CHBT_sizeW = CHBT_sizes[0];
var CHBT_sizeH = CHBT_sizes[1];

CHBT_roomurl = CHBT_SERVER + '/chatroom/' + escape(CHBT_channel) + '/?position=' + CHBT_position;
if (!document.getElementById("CHATBUTTON_CHATBOX")) {
    document.write('<iframe id="CHATBUTTON_CHATBOX" name="CHATBUTTON_CHATBOX" marginwidth="0" marginheight="0" frameborder="0"' + 'vspace="0" hspace="0" allowtransparency="true" scrolling="no" width="' + CHBT_sizeW + '" height="' + CHBT_sizeH + '" ' + 'src="' + CHBT_roomurl + '"></iframe>');
}
if (typeof (CHBT_position) == 'undefined' || !CHBT_position || CHBT_position == '') {
    document.getElementById('CHATBUTTON_CHATBOX').style.position = "fixed";
    document.getElementById('CHATBUTTON_CHATBOX').style.bottom = "0";
    document.getElementById('CHATBUTTON_CHATBOX').style.right = "10px";
}
if (CHBT_position == 'inline') {
    document.getElementById('CHATBUTTON_CHATBOX').style.position = "relative";
    document.getElementById('CHATBUTTON_CHATBOX').style.top = "none";
    document.getElementById('CHATBUTTON_CHATBOX').style.right = "none";
}
if (CHBT_position == 'bottom-right') {
    document.getElementById('CHATBUTTON_CHATBOX').style.position = "fixed";
    document.getElementById('CHATBUTTON_CHATBOX').style.bottom = "0";
    document.getElementById('CHATBUTTON_CHATBOX').style.right = "10px";
}
if (CHBT_position == 'bottom-left') {
    document.getElementById('CHATBUTTON_CHATBOX').style.position = "fixed";
    document.getElementById('CHATBUTTON_CHATBOX').style.bottom = "0";
    document.getElementById('CHATBUTTON_CHATBOX').style.left = "10px";
}
if (CHBT_position == 'top-left') {
    document.getElementById('CHATBUTTON_CHATBOX').style.position = "fixed";
    document.getElementById('CHATBUTTON_CHATBOX').style.top = "0";
    document.getElementById('CHATBUTTON_CHATBOX').style.left = "10px";
}
if (CHBT_position == 'top-right') {
    document.getElementById('CHATBUTTON_CHATBOX').style.position = "fixed";
    document.getElementById('CHATBUTTON_CHATBOX').style.top = "0";
    document.getElementById('CHATBUTTON_CHATBOX').style.right = "10px";
}

document.getElementById('CHATBUTTON_CHATBOX').style.zIndex = "1000000";
CHATROOM_URL = document.write(
  '<form id="CHATBUTTON_LOAD_CHATBOX" name="CHATBUTTON_LOAD_CHATBOX" action="' + CHBT_roomurl + '" target="CHATBUTTON_CHATBOX" method="post">'
+ '<input type="hidden" name="channel" value="' + escape(CHBT_channel) + '">'
+ '<input type="hidden" name="title" value="' + CHBT_title + '">'
+ '<input type="hidden" name="titlecolor" value="' + escape(CHBT_titlecolor) + '">'
+ '<input type="hidden" name="size" value="' + escape(CHBT_size) + '">'
+ '<input type="hidden" name="bgcolor" value="' + escape(CHBT_bgcolor) + '">'
+ '<input type="hidden" name="bordercolor" value="' + escape(CHBT_bordercolor) + '">'
+ '<input type="hidden" name="textsize" value="' + escape(CHBT_textsize) + '">'
+ '<input type="hidden" name="textcolor" value="' + escape(CHBT_textcolor) + '">'
+ '<input type="hidden" name="nncolor" value="' + escape(CHBT_nncolor) + '">'
+ '<input type="hidden" name="usercolor" value="' + escape(CHBT_usercolor) + '">'
+ '<input type="hidden" name="cpcolor" value="' + escape(CHBT_cpcolor) + '">'
+ '<input type="hidden" name="flashcolor" value="' + escape(CHBT_flashcolor) + '">'
+ '<input type="hidden" name="profanityfilter" value="' + escape(CHBT_profanityfilter) + '">'
+ '<input type="hidden" name="loadmessages" value="' + escape(CHBT_loadmessages) + '"></form>');

document.CHATBUTTON_LOAD_CHATBOX.submit();