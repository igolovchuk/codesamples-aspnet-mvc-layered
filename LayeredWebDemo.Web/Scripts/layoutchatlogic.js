
$(function () {

    // Declare a proxy to reference the hub.
    var chatHub = $.connection.chatHub;

    registerClientMethods(chatHub);

    // Start Hub
    $.connection.hub.start().done(function () {

        registerEvents(chatHub)

    });

});


function registerEvents(chatHub) {
  
    //$("#btnStartChat").click(function () {

    var name = $("#txtNickName").val();
    var img = $('#sendimage').text();
    //  var fullname = $("#txtFullkName").val();
    //if (name.length > 0) {
    chatHub.server.connect(name, img);
    //}
    //else {
    //    alert("Please enter name");
    //}

    //});

    $("#txtNickName").keypress(function (e) {
        if (e.which == 13) {
            $("#btnStartChat").click();
        }
    });


}

function registerClientMethods(chatHub) {

    // Calls when user successfully logged in
    chatHub.client.onConnected = function (id, userName, allUsers, messages) {

        //setScreen(true);

        $('#hdId').val(id);
        $('#hdUserName').val(userName);
        $('#spanUser').html(userName);

        // Add All Users
        for (i = 0; i < allUsers.length; i++) {

            AddUser(chatHub, allUsers[i].ConnectionId, allUsers[i].UserName, allUsers[i].Image);
        }

        // Add Existing Messages
        for (i = 0; i < messages.length; i++) {

            AddMessage(messages[i].WindowId, messages[i].UserName, messages[i].Message, messages[i].Image);
        }


    }


    // On New User Connected
    chatHub.client.onNewUserConnected = function (id, name, img) {

        AddUser(chatHub, id, name, img);
    }

    //show last 20 messages
    chatHub.client.receiveHistory = function (chathistory, windowId) {
        if (chathistory != null) {
            // Add Existing Messages
            for (i = 0; i < chathistory.length; i++) {

                GenearteChatHistory(windowId, chathistory[i].FromUserName, chathistory[i].Message, chathistory[i].Image);
            }
        }
    };

    //say who is typing
    chatHub.client.sayWhoIsTyping = function (name) {
        $('#isTyping').text(name + ' is typing...');
        setTimeout(function () {
            $('#isTyping').text(' ');
        }, 3500);
    };

    // On User Disconnected
    chatHub.client.onUserDisconnected = function (id, userName) {

        $('#' + id).remove();

        var ctrId = 'private_' + id;
        $('#' + ctrId).remove();

        //to show when user is logged off
        //var disc = $('<div class="disconnect">"' + userName + '" logged off.</div>');

        //$(disc).hide();
        //$('#divusers').prepend(disc);
        //$(disc).fadeIn(200).delay(2000).fadeOut(200);

    }

    chatHub.client.messageReceived = function (userName, message) {

      //  AddMessage(userName, message);
    }


    chatHub.client.sendPrivateMessage = function (windowId, fromUserName, message, image) {

        var ctrId = 'private_' + windowId;
        var count = 0;

        if ($('#' + ctrId).length == 0) {

            count = 1;
            createPrivateChatWindow(chatHub, windowId, ctrId, fromUserName);

        }
        var msg = "";
        if (fromUserName == $('#txtNickName').val()) {
            msg = $('<div class="message clearfix"><div class="chat-bubble from-me">' +
               message +
            '</div></div>');
        }
        else {
            msg = $('<div class="message clearfix"><div class="profile-img-wrapper m-t-5 inline">' +
            '<img class="col-top" width="30" height="30" src="' + image + '" alt="">' +
            '</div><div class="chat-bubble from-them">' +
                 message +
              '</div></div>');
           
           
        }
       
        $('#txtPrivateMessage').keypress(function (e) {
            if (e.which == 13) {
                $('#btnSendMessage').trigger('click');
            } else {
                var encodedName = $('#txtNickName').val();
                chatHub.server.isTyping(windowId, encodedName);
            }
        });
        if (count != 1) {
            $('#' + ctrId).find('#divMessage').append(msg);
        }
        //$('#' + ctrId).find('#divMessage').append('<div class="message"><span class="userName">' + fromUserName + '</span>: ' + message + '</div>');
        // set scrollbar
        var height = $('#' + ctrId).find('#divMessage')[0].scrollHeight;
        $('#' + ctrId).find('#divMessage').scrollTop(height);

    }

    

}
var firstletter = '';
function AddUser(chatHub, id, name, img) {

    var userId = $('#hdId').val();

    var code = "";

    if (userId == id) {

        //  code = $('<div class="loginUser">' + name + "</div>");

    }
    else {
        if (name.charAt(0) == firstletter) {
            code = $(
             '<ul><li class="chat-user-list clearfix">' +
            '<a id="' +
            id +
            '" class="user" >' +
            '<span class="col-xs-height col-middle"><span class="thumbnail-wrapper d32 circular bg-success">' +
            '<img width="34" height="34" alt="" src="' + img + '" class="col-top">' +
              '</span></span><p class="p-l-10 col-xs-height col-middle col-xs-12"><span class="text-master">' +
               name +
            '</span> <span class="block text-master hint-text fs-12">type to send msg...</span></p></a></li></ul>');
        }
        else {
            code = $(
                             '<div id="' + id + '" class="list-view-group-container"><div class="list-view-group-header text-uppercase alphabet">' + name.charAt(0) + '</div><ul><li class="chat-user-list clearfix">' +
                            '<a id="' +
                            id +
                            '" class="user" >' +
                            '<span class="col-xs-height col-middle"><span class="thumbnail-wrapper d32 circular bg-success">' +
                            '<img width="34" height="34" alt="" src="' + img + '" class="col-top">' +
                              '</span></span><p class="p-l-10 col-xs-height col-middle col-xs-12"><span class="text-master">' +
                               name +
                            '</span> <span class="block text-master hint-text fs-12">type to send msg...</span></p></a></li></ul></div>');
            firstletter == name.charAt(0);
        }


        $(code).click(function () {

            var id = $(this).attr('id');

            if (userId != id)
                OpenPrivateChatWindow(chatHub, id, name);

        });
    }

    $("#divusers").append(code);

}


function GenearteChatHistory(ctrId, fromUserName, message, image) {
  
    //if ($('#' + ctrId).length == 0) {

    //    createPrivateChatWindow(chatHub, windowId, ctrId, fromUserName);

    //}
    var msg = "";
    if (fromUserName == $('#txtNickName').val()) {
        msg = $('<div class="message clearfix"><div class="chat-bubble from-me">' +
           message +
        '</div></div>');
    }
    else {
        msg = $('<div class="message clearfix"><div class="profile-img-wrapper m-t-5 inline">' +
        '<img class="col-top" width="30" height="30" src="' + image + '" alt="">' +
        '</div><div class="chat-bubble from-them">' +
             message +
          '</div></div>');


    }

    $('#' + ctrId).find('#divMessage').append(msg);
    //$('#' + ctrId).find('#divMessage').append('<div class="message"><span class="userName">' + fromUserName + '</span>: ' + message + '</div>');
    // set scrollbar
    var height = $('#' + ctrId).find('#divMessage')[0].scrollHeight;
    $('#' + ctrId).find('#divMessage').scrollTop(height);
}

function OpenPrivateChatWindow(chatHub, id, userName) {

    var ctrId = 'private_' + id;

    if ($('#' + ctrId).length > 0) { $('#' + ctrId).show(); return; }

    createPrivateChatWindow(chatHub, id, ctrId, userName);

}

function createPrivateChatWindow(chatHub, userId, ctrId, userName) {

    //var div = '<div id="' + ctrId + '" class="ui-widget-content draggable" rel="0">' +
    //           '<div class="header">' +
    //              '<div  style="float:right;">' +
    //                  '<img id="imgDelete"  style="cursor:pointer;" src="/Images/delete.png"/>' +
    //               '</div>' +

    //               '<span class="selText" rel="0">' + userName + '</span>' +
    //           '</div>' +
    //           '<div id="divMessage" class="messageArea">' +

    //           '</div>' +
    //           '<div class="buttonBar">' +
    //              '<input id="txtPrivateMessage" class="msgText" type="text"   />' +
    //              '<input id="btnSendMessage" class="submitButton button" type="button" value="Send"   />' +
    //           '</div>' +
    //        '</div>';
    chatHub.server.getHistory(ctrId, userName, $('#txtNickName').val());
    var div = '<div id="' + ctrId + '" class="view chat-view bg-white clearfix">' +
        '<div class="navbar navbar-default"><div class="navbar-inner">' +
        '<a href="javascript:;" id="imgDelete" class="link text-master inline action p-l-10 p-r-10" data-navigate="view" data-view-port="#chat" data-view-animation="push-parrallax">' +
            '<i class="pg-arrow_left"></i></a><div class="view-heading" id="talker">' + userName +
         '</div></div></div>' +
         '<div class="chat-inner" id="divMessage"></div><em id="isTyping" style="height: 20px; display: block;padding-left: 10px;padding-bottom: 5px;"></em>' +
        '<div class="b-t b-grey bg-white clearfix p-l-10 p-r-10"><div class="row"><div class="col-xs-1 p-t-15">' +
            '<a href="#" id="btnSendMessage" class="link text-master"><i class="fa fa-plus-circle"></i></a></div>' +
       '<div class="col-xs-8 no-padding">' +
            '<input type="text" id="txtPrivateMessage" class="form-controller chat-input" data-chat-input="" data-chat-conversation="#my-conversation" placeholder="Say something">' +
        '</div><div class="col-xs-2 link text-master m-l-10 m-t-15 p-l-10 b-l b-grey col-top"><a href="#" class="link text-master"><i class="pg-camera"></i></a></div></div></div></div>';

    var $div = $(div);

    // DELETE BUTTON IMAGE
    $div.find('#imgDelete').click(function () {
        $('#' + ctrId).hide();
    });

    // Send Button event
    $div.find("#btnSendMessage").click(function () {

        $textBox = $div.find("#txtPrivateMessage");
        var msg = $textBox.val();
        var img = $('#sendimage').text();
        if (msg.length > 0) {

            chatHub.server.sendPrivateMessage(userId, msg, img);
            $textBox.val('');
        }
    });

    // Text Box event
    $div.find("#txtPrivateMessage").keypress(function (e) {
        if (e.which == 13) {
            $div.find("#btnSendMessage").click();
        }
    });
    
   

    AddDivToContainer($div);
    
    

}

function AddDivToContainer($div) {
    //$('#divContainer').prepend($div);
    $('#chatter').prepend($div);
    //$div.draggable({

    //    handle: ".header",
    //    stop: function () {

    //    }
    //});

    ////$div.resizable({
    ////    stop: function () {

    ////    }
    ////});

}
