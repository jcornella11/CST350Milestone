$(function ()
{
    //Load the Inital Blank Game Board
    updateAllButtons();
    getGameTableData();

    //Left Click on Game Button
    $(document).on("click", ".game-button", function (event) {
        event.preventDefault();
        
        var buttonCordinates = $(this).val();
        var buttonX = $(this).data("valuex");
        var buttonY = $(this).data("valuey");

        doButtonUpdate(buttonX, buttonY, buttonCordinates);
        updateAllButtons();
        getGameTableData();
    });

    //Right Click on Game Button
    $(document).on("contextmenu", ".game-button", function (event) {
        event.preventDefault();

        var buttonCordinates = $(this).val();
        var buttonX = $(this).data("valuex");
        var buttonY = $(this).data("valuey");

        doButtonRightClick(buttonX, buttonY, buttonCordinates);
        updateAllButtons();
        getGameTableData();
    });

    //New Game Triggered After Game Difficulty is Selected
    $(document).ready(function () {
        $("#newgame").click(function () {
            var gameDifficulty = $("input[name='gameDifficulty']:checked").val();

            $.ajax(
                {
                    datatype: "json",
                    method: 'POST',
                    url: 'game/NewGame',
                   data:
                    {
                        "difficulty": gameDifficulty
                    },
                    success: function () {
                        updateAllButtons();
                        getGameTableData();
                    }
                });
            console.log(gameDifficulty);
        });
    });

    //Save Function Triggered When Button is Pressed
    $(document).ready(function () {
        $("#savegame").click(function () {
  
            $.ajax(
                {
                    datatype: "json",
                    method: 'POST',
                    url: 'game/SaveGame',
                    data:
                    {
                        
                    },
                    success: function (data)
                    {
                        $("#savedgametable").html(data);
                    }
                });
           
        });
    });

    //Load Function Triggered when button is pressed
    $("#savedgametable").on('click','td', function () {

            var buttonId = $("#loadgame").val();
            console.log(buttonId);
            
                $.ajax(
                    {
                        datatype: "json",
                        method: 'POST',
                        url: 'game/LoadGame',
                        data:
                        {
                            "id": buttonId
                        },
                        success: function () {
                            updateAllButtons();
                            getGameTableData();
                        }
                    });
    });

    //Delete Function Triggered when button is pressed
    $("#savedgametable").on('click', 'td', function () {

        var buttonId = $("#deletegame").val();
        console.log(buttonId);

        $.ajax(
            {
                datatype: "json",
                method: 'POST',
                url: 'game/DeleteGame',
                data:
                {
                    "id": buttonId
                },
                success: function (data) {
                    $("#savedgametable").html(data);
                    updateAllButtons();
                    getGameTableData();
                }
            });
    });



});

//Ajax to Show One Button
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

//Right Click and Flag One Button
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