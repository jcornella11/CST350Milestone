$(function ()
{
    //Load the Inital Blank Game Board
    updateAllButtons();
    getGameTableData();

    //Left Click
    $(document).on("click", ".game-button", function (event) {
        event.preventDefault();
        
        var buttonCordinates = $(this).val();
        var buttonX = $(this).data("valuex");
        var buttonY = $(this).data("valuey");

        doButtonUpdate(buttonX, buttonY, buttonCordinates);
        updateAllButtons();
        getGameTableData();
    });

    //Right Click
    $(document).on("contextmenu", ".game-button", function (event) {
        event.preventDefault();

        var buttonCordinates = $(this).val();
        var buttonX = $(this).data("valuex");
        var buttonY = $(this).data("valuey");

        doButtonRightClick(buttonX, buttonY, buttonCordinates);
        updateAllButtons();
        getGameTableData();
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
                $("#" + buttonCordinates).html(data);
            }
        });
};


function doButtonRightClick(buttonX, buttonY, buttonCordinates)
{
    $.ajax(
        {
            datatype: "json",
            method: 'POST',
            url: 'game/doButtonRightClick',
            data:
            {
                "buttonXCordinate": buttonX,
                "buttonYCordinate": buttonY
            },
            success: function (data) {
                $("#" + buttonCordinates).html(data);
            }
        });
}

function updateAllButtons()
{
    
            $.ajax({
                datatype: "json",
                method: 'POST',
                url: 'game/ShowAllButtons',
                data:
                {
                },
                success: function (data)
                {
                    $("#gameGrid").html(data);
                }

            });
      
};

function getGameTableData()
{
    $.ajax(
        {
            datatype: "json",
            method: 'POST',
            url: 'game/ShowGameData',
            data:
            {},
            success: function (data) {
                $("#gameTable").html(data);
            }
        });
};