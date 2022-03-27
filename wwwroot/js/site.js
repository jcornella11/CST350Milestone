$(function ()
{
    console.log("Page is ready");

    $(document).on("click", ".game-button", function (event) {
        event.preventDefault();
        console.log("Game Button was Clicked");

        var buttonCordinates = $(this).val();
        var buttonX = $(this).data("valuex");
        var buttonY = $(this).data("valuey");
        console.log("Button Number " + buttonCordinates + " was clicked");
        console.log("Button X Atribute: " + buttonX);
        console.log("Button Y Atribute: " + buttonY);
        doButtonUpdate(buttonX, buttonY, buttonCordinates);

        for (let i = 0; i < 10; i++)
        {
            for (let j = 0; j < 10; j++)
            {
                doButtonUpdate(i, j);
            }
        }
    });
});

function doButtonUpdate(buttonX, buttonY, buttonCordinates)
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
            success: function (data) {
                console.log(data);
                console.log(buttonCordinates);
                $("#" + buttonCordinates).html(data);
            }
        });
};