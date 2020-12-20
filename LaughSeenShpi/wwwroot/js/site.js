// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


$("#SendMessageButton").click(function () {
    $.ajax({
        type: "POST",
        url: "/api/ReadWriteMessages",
        data: JSON.stringify({
            Content: $("#MessageContent").val(),
            RoomMemberId: parseInt($("#MessageRoomMemberId").val())
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



// Get the input field
var input = document.getElementById("MessageContent");
var objDiv = document.getElementById("messageHistoryDiv");

var options = { weekday: 'long'};
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