var reciverId;
var userId;
function SetReciverId(recvId) {
    reciverId = recvId;
}
function SetuserId() {
    userId = document.getElementById("UserID").value

}
SetuserId();
function RemoveChat(recvId) {
    document.getElementById("chatbody").remove()
    reciverId = recvId;

}
$(document).ready(() => {

});

    $.connection.hub.start();
    let hub = $.connection.myHub;
hub.client.newMessage = (sendId, msg, divId) => {
    console.log(sendId)
    console.log(msg)
    console.log(divId)
    div = "#" + divId
    var today = new Date();
    var date = today.getFullYear() + '-' + (today.getMonth() + 1) + '-' + today.getDate();
    var time = today.getHours() + ":" + today.getMinutes() 
    if (sendId === userId) {
       
        console.log("#############")
        $(div).append(`<div class="incoming_msg">
    <div class="incoming_msg_img"> <img src="https://ptetutorials.com/images/user-profile.png" alt="sunil"> </div>
    <div class="received_msg">
        <div class="received_withd_msg">
            <p>
                ${msg}
            </p>
            <span class="time_date"> ${time}    |    ${date}</span>
        </div>
    </div>
</div>`)
    }
    else {
        if (document.getElementById(divId)!==null) {

        console.log("*******")
        $(div).append(`<div class="outgoing_msg">
    <div class="sent_msg">
        <p>
            ${msg}
        </p>
         <span class="time_date"> ${time}    |    ${date}</span>
         <span class="time_date">Seen</span>
    </div>
</div>`)
        }

            
    }

    }
    $("#send").click(() => {
        const message = $("#txt").val();
      
        hub.server.send(reciverId, message)
    })