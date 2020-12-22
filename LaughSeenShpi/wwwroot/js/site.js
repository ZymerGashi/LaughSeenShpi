// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


//Add new messages to the chat

var pusher = new Pusher('07088996639e59625df1', {
    cluster: 'eu',
    encrypted: true
});


$("#SendMessageButton").click(function () {
    $.ajax({
        type: "POST",
        url: "/api/ReadWriteMessages",
        data: JSON.stringify({
            Content: $("#MessageContent").val(),
            RoomMemberId: parseInt($("#MessageRoomMemberId").val()),
            socketId: pusher.connection.socket_id
        }),
        success: (data) => {
            $(".msg_history").append(`<div class="outgoing_msg" >
                                            <div class="sent_msg" style="word-wrap:hyphenate">
                                                <p>`
                + data.data.content + `</p>
                                                <span class="time_date">` + formatDate(data.data.sendTime)+

                                            `</span>
                                            </div>
                                        </div>`
            );

            $("#MessageContent").val('');
            scrollDown();
        },
        dataType: 'json',
        contentType: 'application/json'
    });
});



// When a user clicks on a group, Load messages for that particular group.
$("#messageHistoryDiv").ready(function () {




    let roomId = $("#RoomId").val();

    //$('.group').css({ "border-style": "none", cursor: "pointer" });
    //$(this).css({ "border-style": "inset", cursor: "default" });

    //$("#currentGroup").val(group_id); // update the current group_id to html file...

    // get all messages for the group and populate it...
    $.get("/api/ReadWriteMessages/" + roomId, function (data) {
        let message = "";

        data.forEach(function (data) {

            message += `<div class="incoming_msg" > <div class="incoming_msg_img"> <img src="https://ptetutorials.com/images/user-profile.png" alt="sunil"> </div>
                                            <div class="received_msg" style="word-wrap:hyphenate">
<div class="received_withd_msg" style="word-wrap:hyphenate">
<span class="small"  style="padding-top:0">` +data.roomMembers.memberName + `  </span>
                                                <p>`
                + data.content + `</p>
                                                <span class="time_date">` + formatDate(data.sendTime) +

                `</span>
</div>
                                            </div>
                                        </div>`;
        });

        $(".msg_history").append(message);


        pusher.unsubscribe(roomId); //unsubscribe

        let group_channel = pusher.subscribe(roomId);

            group_channel.bind('new_message', function (data) {
                if (roomId == data.messagePlusRoom.room.ID) {
                    $(".msg_history").append(`<div class="incoming_msg" > <div class="incoming_msg_img"> <img src="https://ptetutorials.com/images/user-profile.png" alt="sunil"> </div>
                                            <div class="received_msg" style="word-wrap:hyphenate">
<div class="received_withd_msg" style="word-wrap:hyphenate">
<span class="small"  style="padding-top:0">` + data.messagePlusRoom.messages.RoomMembers.MemberName + `  </span>
                                                <p>`
                        + data.messagePlusRoom.messages.Content + `</p>
                                                <span class="time_date">` + formatDate(data.messagePlusRoom.messages.SendTime) +

                        `</span>
</div>
                                            </div>
                                        </div>`);
                }


                scrollDown();
            });
    




    });

});










// Get the input field
var input = document.getElementById("MessageContent");
var objDiv = document.getElementById("messageHistoryDiv");

var options = {  month: 'long', day: 'numeric',hour: 'numeric',minute: 'numeric'};
// Execute a function when the user releases a key on the keyboard
input.addEventListener("keyup", function (event) {
    // Number 13 is the "Enter" key on the keyboard
    if (event.keyCode === 13) {
        // Cancel the default action, if needed
        event.preventDefault();
        // Trigger the button element with a click
        document.getElementById("SendMessageButton").click();


    }
});



//Scroll down the chat div
var scrollDown = function (response) {
    /* do something here */
    objDiv.scrollTop = objDiv.scrollHeight;
}


var formatDate = function (date) {
/* do something here */
    var formatedDate = new Date(date);
    return formatedDate.toLocaleString("en-US", options);
}

