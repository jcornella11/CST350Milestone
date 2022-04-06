$(function ()
{
    $(document).on("click", ".game-button", function (event) {
        event.preventDefault();
        
        var buttonCordinates = $(this).val();
        var buttonX = $(this).data("valuex");
        var buttonY = $(this).data("valuey");
        
        doButtonUpdate(buttonX, buttonY, buttonCordinates);
        getGameTableData();
    });

    $(document).on("contextmenu", ".game-button", function (event) {
        event.preventDefault();

        var buttonCordinates = $(this).val();
        var buttonX = $(this).data("valuex");
        var buttonY = $(this).data("valuey");
        console.log(buttonX);
        console.log(buttonY);
        doButtonRightClick(buttonX, buttonY, buttonCordinates);
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
                updateAllButtons();
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
                console.log(data);
                updateAllButtons();
            }
        });
}

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
                success: function (data)
                {
                    $("#" + i + j).html(data);
                }

            });
        }
    }
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