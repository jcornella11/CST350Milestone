$(function ()
{
    console.log("Page is ready");

    $(".game-button").click(function (event) {
        event.preventDefault();
        console.log("Game Button was Clicked");

        var buttonCordinates = $(this).val();
        var buttonX = $(this).data("valuex");
        var buttonY = $(this).data("valuey");
        console.log("Button Number " + buttonCordinates + " was clicked");
        console.log("Button X Atribute: " + buttonX);
        console.log("Button Y Atribute: " + buttonY);
    });
});

function doButtonUpdate(buttonX, buttonY)
{
    $.ajax(
        {
            datatype: "json",
            method: 'POST',
            url: 'game/ShowOneButton',
            data:
            {
                "buttonXCordinate": buttonX,
                "buttonYCordinate": buttonY
            },
            success: function (data)
            {
                console.log(data);
            }
        })
}