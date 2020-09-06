function LeaveComment(id) {
    localStorage.setItem("id", id);
    window.location.hash = "respond";
}
function SubmitComment(id) {

    var parentId = localStorage.getItem("id");
    //var url = window.location.pathname;
    //var id = url.substring(url.lastIndexOf('/') + 1);
    var nameVal = $("#commentName").val();
    var emailVal = $("#commentEmail").val();
    var bodyVal = $("#commentBody").val();
    if (nameVal !== "" && emailVal !== "" && bodyVal !== "") {
        $.ajax(
            {
                url: "/Pages/PostSubmitComment",
                data: { name: nameVal, email: emailVal, body: bodyVal, id: id, parentId: parentId },
                type: "POST"
    
            }).done(function (result) {
                if (result === "true") {
                    $("#errorDivQ").css('display', 'none');
                    $("#SuccessDivQ").css('display', 'block');
                    localStorage.setItem("id", "");
                }
                else if (result === "InvalidEmail") {
                    $("#errorDivQ").html('ایمیل وارد شده صحیح نمی باشد.');
                    $("#errorDivQ").css('display', 'block');
                    $("#SuccessDivQ").css('display', 'none');

                }
            });
    }
    else {
        $("#errorDivQ").html('تمامی فیلد های زیر را تکمیل نمایید.');
        $("#errorDivQ").css('display', 'block');
        $("#SuccessDivQ").css('display', 'none');

    }
}
