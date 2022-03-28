$(function ()
{
    console.log("Page is ready");
    document.addEventListener('contextmenu', event => event.preventDefault());
    
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
    });
});

function right_click(clicked_id) {
    if (event.which == 3) {
        var button = document.getElementById(clicked_id);

        if (typeof button.src === 'undefined') {
            $(button).attr('src', "http://icongal.com/gallery/image/37201/cancel_exit_cross_close.png");
            /*document.getElementById(clicked_id).src = "http://icongal.com/gallery/image/37201/cancel_exit_cross_close.png";*/
            console.log("Undefined");
        }

        updateAllButtons();
        console.log(clicked_id);
    }
}

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
                //console.log(data);
                //console.log(buttonCordinates);
                $("#" + buttonCordinates).html(data);
                updateAllButtons();
            }
        });
    
};

function updateAllButtons()
{
    for (let i = 0; i < 10; i++)
    {
        for (let j = 0; j < 10; j++)
        {
            $.ajax({
                datatype: "json",
                method: 'POST',
                url: 'game/ShowAllButtons',
                data:
                {
                    "buttonXCordinate": i,
                    "buttonYCordinate": j
                },
                success: function (data) {
                    //console.log(data);
                    $("#" + i + j).html(data);
                }

            });
        }
    }
};