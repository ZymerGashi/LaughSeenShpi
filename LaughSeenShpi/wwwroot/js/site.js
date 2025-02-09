﻿// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
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
        url: "/api/ReadWriteMessages/AddMessages",
        data: JSON.stringify({
            Content: $("#MessageContent").val(),
            RoomMemberId: parseInt($("#MessageRoomMemberId").val()),
            socketId: pusher.connection.socket_id
        }),
        success: (data) => {
            $(".msg_history").append(`<div class="outgoing_msg" >
                                            <div class="sent_msg" style="word-wrap:break-word ">
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
    $.get("/api/ReadWriteMessages/GetMessages/" + roomId, function (data) {
        let message = "";




        data.forEach(function (data) {

            message += `<div class="incoming_msg" > <div class="incoming_msg_img"> <img src="https://ptetutorials.com/images/user-profile.png" alt="sunil"> </div>
                                            <div class="received_msg" style="word-wrap:break-word">
<div class="received_withd_msg" style="word-wrap:break-word">
<span class="small text-white font-italic"  style="padding-top:0">` +data.roomMembers.memberName + `  </span>
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
                                            <div class="received_msg" style="word-wrap:break-word">
<div class="received_withd_msg " style="word-wrap:break-word">
<span class="small text-white font-italic"  style="padding-top:0">` + data.messagePlusRoom.messages.RoomMembers.MemberName + `  </span>
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



        group_channel.bind('video_parameters_changed', function (data) {

            //alert('VideoParametersHave changed');

            changeCurrentVideoTime(data.roomMember.Room.CurrentTime);

            if (data.roomMember.Room.PlayTheMovie == false) {
                PauseTheVideo();

                $(".msg_history").append(`<div class="col-sm-12 text-white-50 text-sm-center">
                                                        <span><i>`+ data.roomMember.MemberName + ` paused the video
</i> </span>


                                                    </div>
`);

            }
            else {
                PlayTheVideo();
                $(".msg_history").append(`<div class="col-sm-12 text-white-50 text-sm-center">
                                                        <span><i>`+ data.roomMember.MemberName + ` played the video
</i> </span>


                                                    </div>
`);


            }


            scrollDown();

        });

    });

});

var input = document.getElementById("MessageContent");
var objDiv = document.getElementById("messageHistoryDiv");
var video = document.getElementById("movieVideo");



//$("#movie").mouseup(function () {
//    $.ajax({
//        type: "POST",
//        url: "/api/ReadWriteMessages/UpdateRoom/",
//        data: JSON.stringify({
//            ID: parseInt($("#RoomId").val()),
//            CurrentTime: video.currentTime,
//            PlayTheMovie: video.paused
//        }),
//        dataType: 'json',
//        contentType: 'application/json'
//    });
//});




$("#movie").mouseup(function () {
    $.ajax({
        type: "POST",
        url: "/api/ReadWriteMessages/UpdateRoom/",
        data: JSON.stringify({
            MemberID: parseInt($("#MessageRoomMemberId").val()),
            MemberName: $("#MessageRoomMemberName").val(),
            MemberRoomId: parseInt($("#RoomId").val()),
            Room:
            {
                ID: parseInt($("#RoomId").val()),
                CurrentTime: video.currentTime,
                PlayTheMovie: video.paused
            }
        }),
        dataType: 'json',
        contentType: 'application/json'
    });
});





//This is how I can use the onplay and onpause events so I can handle also the case when the user plays/pauses the video from the video controls. The drawback is that it causes loops...

//$("#movieVideo").on('play', function () {

//    $.ajax({
//        type: "POST",
//        url: "/api/ReadWriteMessages/UpdateRoom/",
//        data: JSON.stringify({
//            ID: parseInt($("#RoomId").val()),
//            CurrentTime: video.currentTime,
//            PlayTheMovie: true
//        }),
//        dataType: 'json',
//        contentType: 'application/json'
//    });
//});

//$("#movieVideo").on('pause', function () {

//    $.ajax({
//        type: "POST",
//        url: "/api/ReadWriteMessages/UpdateRoom/",
//        data: JSON.stringify({
//            ID: parseInt($("#RoomId").val()),
//            CurrentTime: video.currentTime,
//            PlayTheMovie: false
//        }),
//        dataType: 'json',
//        contentType: 'application/json'
//    });
//});









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

var options = { month: 'long', day: 'numeric', hour: 'numeric', minute: 'numeric' };
var formatDate = function (date) {
/* do something here */
    var formatedDate = new Date(date);
    return formatedDate.toLocaleString("en-US", options);
}

var PauseTheVideo = function () {
    video.pause();
}

var PlayTheVideo = function () {
    video.play();
}

var changeCurrentVideoTime = function (newCurrentTime)
{
    video.currentTime = newCurrentTime;
}

