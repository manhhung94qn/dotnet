$(document).ready(function () {
    $("#map").hide()
    renderData();
});

//================= Render Data GasStation ============//
let dataSend = {
    Page: curentPage,
    GasName: "",
    GasTypes: [],
    District: undefined
}
let renderData = (isRenderPagination = true) => {
    $.ajax({
        type: "POST",
        url: "/api/GasStationAPI/GetAllGasStations",
        data: JSON.stringify(dataSend),
        contentType: 'application/json; charset=utf-8',
        success: function (response) {
            $("#table-body-content").html(renderTableBodyForGasStation(response.gasStaionMVs));
            if (isRenderPagination) {
                setPagination(response.count);
            }
        }
    });

}

//================= Setup table ========================//
let renderTableBodyForGasStation = (data = []) => {
    let result = ``;
    for (const item of data) {
        result += ` 
            <tr>
                <td class="pl-lg-2 text-lg-left gas-station-name" 
                    data-name="${item.gasStationName}" 
                    data-longitude="${item.longitude}" 
                    data-latitude="${item.latitude}"
                    data-rating=${item.rating.name}
                >${item.gasStationName}</td>
                <td>${compileGasType(item.types)}</td>
                <td>${item.district ? item.district.districtName : ""}</td>
                <td>${item.longitude + ", " + item.latitude}</td>
                <td> <img src="/images/${item.rating.name}.png" /></td>
                <td>
                    <button type="button" class="btn btn-primary">Edit</button>
                    <button type="button" class="btn btn-danger">Delete</button>
                </td>
            </tr>
            `
    }
    return result;
}

let compileGasType = data => {
    let result = null;
    for (const item of data) {
        if (result) {
            result += "," + item.gasTypeName;
        } else { result = item.gasTypeName }
    }
    return result;
}


//=============== Setup Pagination =============//

let setPagination = (countPage) => {
    let result = "";
    if (countPage == 0) {
        result = ``;
    } else {
        result = `
        <nav aria-label="...">
            <ul class="pagination">
                <li class="page-item disabled  controll controllerFirst cursor">
                    <span class="page-link cursor"><<</span>
                </li>
                <li class="page-item disabled  controll controllerPre cursor">
                    <span class="page-link"><</span>
                </li>
                <li class="page-item active"><span class="page-link cursor">1</span></li>   `;
        if (countPage > 1) {
            for (let i = 2; i <= Math.ceil(countPage / 10); i++) {
                result += `<li class="page-item"><span class="page-link cursor">${i}</span></li>`
            }
        }

        if (countPage <= 10) {
            result += `
        <li class="page-item disabled controll controllerNext cursor">
            <span class="page-link">></span>
        </li>
        <li class="page-item disabled  controll controllerLast cursor">
            <span class="page-link">>></span>
        </li> `
        } else {
            result += `
        <li class="page-item controll controllerNext cursor">
            <span class="page-link">></span>
        </li>
        <li class="page-item  controll controllerLast cursor">
            <span class="page-link">>></span>
        </li>
        `
        }
        result += ` </ul> </nav>`
    }

    $("#pagination-box").html(result);
}
var curentPage = 1;
$("body").delegate(".page-item", "click", function () {
    let setPage;
    if ($(this).hasClass("controll")) {
        if (!$(this).hasClass("disabled")) {
            if ($(this).hasClass("controllerPre")) {
                setPage = curentPage - 1;
            }
            if ($(this).hasClass("controllerNext")) {
                setPage = curentPage + 1
            }
            if ($(this).hasClass("controllerFirst")) {
                setPage = 1
            }
            if ($(this).hasClass("controllerLast")) {
                setPage = $(".page-item").length - 4;
            }
        } else { return }

    } else {
        setPage = parseInt($(this).children().html()) ? parseInt($(this).children().html()) : curentPage;
    }

    if (setPage != curentPage) {
        let $listPageItem = $(".page-item");
        for (const item of $listPageItem) {
            if ($(item).children().html() == setPage) {
                $(item).addClass("active")
            } else { $(item).removeClass("active") }
        }
        if (setPage == 1) {
            $($listPageItem[0]).addClass("disabled");
            $($listPageItem[1]).addClass("disabled");
        } else { $($listPageItem[0]).removeClass(" disabled"); $($listPageItem[1]).removeClass(" disabled") }
        if (setPage == $listPageItem.length - 4) {
            $($listPageItem[$listPageItem.length - 2]).addClass("disabled");
            $($listPageItem[$listPageItem.length - 1]).addClass("disabled")
        } else {
            $($listPageItem[$listPageItem.length - 2]).removeClass("disabled");
            $($listPageItem[$listPageItem.length - 1]).removeClass("disabled")
        }
        curentPage = setPage;

    }
    dataSend.Page = curentPage;

    renderData(false)
});

//=============== Setup Search =================//
$("#btn-search")
$("body").delegate("#btn-search", "click", (e) => {
    e.preventDefault();
    dataSend.Page = 1;
    dataSend.GasName = $("#gas-station-name").val().toLowerCase();
    dataSend.District = parseInt($("#select-district").val()) ? parseInt($("#select-district").val()) : 0;
    dataSend.GasTypes = [];
    let list = $("input[name='select-gas-type']:checked");
    for (const item of list) {
        dataSend.GasTypes.push($(item).val())
    }
    renderData();
});

//=============== Setup map ===================//

var longiTude = 150.644;
var latiTude = -34.397;
var ratingIcon = "mid"
function initMap() {
    var uluru = { lat: latiTude, lng: longiTude };
    var map = new google.maps.Map(document.getElementById('map-content'), {
        zoom: 12,
        center: uluru
    });
    var marker = new google.maps.Marker({
        position: uluru,
        map: map,
        icon:`/images/${ratingIcon}.png`
    });
}

let ishowMap = false;
var cleartime = null;
$("body").delegate(".gas-station-name", "mouseover", function (e) {
    $("#map").hide();
    $("#map-name").text($(this).data().name)
    longiTude = $(this).data().longitude;
    console.log(longiTude)
    latiTude = $(this).data().latitude;
    ratingIcon = $(this).data().rating;
    initMap();
    $("#map").css("left", e.pageX - $("#map").width() / 2).css("top", e.pageY - $("#map").height() - 20);
    cleartime = setTimeout(function () { $("#map").show() }, 500);
});

$("body").delegate("#map", "mouseout", function (e) {
    ishowMap = true;
});
$("body").delegate("#map", "mouseout", function (e) {
    setTimeout(function () { if (ishowMap) { ishowMap = false; } }, 500);
    $("#map").hide();
});

$("body").delegate(".gas-station-name", "mouseout", function (e) {
    if (!ishowMap) {
        clearTimeout(cleartime);
        $("#map").hide();
    }
});


//================= Setup delete =======================//
$("body").delegate(".cofirm-delete", "click", function () {
    let id = $(this).data().id;
    let text = "ガソリンスタンド" + $(this).data().name + "は削除されますか？"
    if (confirm(text)) {
        $.ajax({
            type: "POST",
            url: localStorage.getItem("baseUrl") + "/Gasstation/RemoveGas/" + id,
            success: function (response) {
                if (response.status) {
                    let token = $("[name=__RequestVerificationToken]").val();
                    renderData(token);
                } else {
                    alert("Gas station is not exist")
                }
            }
        });
    } else {

    }
}
)

