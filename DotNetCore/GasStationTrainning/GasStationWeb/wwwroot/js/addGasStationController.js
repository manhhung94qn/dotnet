$(document).ready(function () {
    $(($("input[name='rating-gas']"))[1]).attr("checked", "checked");
});

let dataSend = {
    GasStationName: "Hung",
    Types: [],
    Longitude: undefined,
    Latitude: undefined,
    District: {
        DistrictId: undefined
    },
    OpeningTime: undefined,
    Rating: {
        GasTypeCode: undefined
    },
    Address: "",
}
let sendDataGasStation = () => {
    $.ajax({
        type: "POST",
        url: "/api/GasStationAPI/AddGasStation",
        data: JSON.stringify(dataSend),
        contentType: 'application/json; charset=utf-8',
        success: function (response) {
            console.log(response)
        }
    });
}

let setDataSend = () => {
    dataSend.GasStationName = $("#gas-station-name").val();
    let list = $("input[name='select-gas-type']:checked");
    if (list.length == 0) {
        return false;
    }
    dataSend.Types = [];
    for (const item of list) {
        dataSend.Types.push({ GasTypeCode: $(item).val() })
    }

    if (!$("#gas-station-longitude").val()) { return false; }
    dataSend.Longitude = parseInt($("#gas-station-longitude").val().substring(0, 11));
    if (!$("#gas-station-latitude").val()) {
        return false;
    };
    dataSend.Latitude = parseInt($("#gas-station-latitude").val());
    if (!$("#select-district").val()) {
        return false;
    }
    dataSend.District.DistrictId = parseInt($("#select-district").val());
    if (!$("#gas-station-opening-time").val()) {
        return false;
    }
    dataSend.OpeningTime = $("#gas-station-opening-time").val();
    dataSend.Rating.Code = $('input[name="rating-gas"]:checked').val();
    if (!$("#gas-station-address").val()) {
        return false;
    }
    dataSend.Address = $("#gas-station-address").val();
    return true;
}
let boot = false;
$("#submit-add-gas-station").click(e => {
    e.preventDefault();
    boot = true;
    customValidater();
    let isValid = setDataSend();
    if (isValid) {
        sendDataGasStation();
    }
})

$("body").delegate("#gas-station-longitude", "keydown", e => {
    isNumberKey(e, "#gas-station-longitude")
})
$("body").delegate("gas-station-latitude", "keydown", e => {
    isNumberKey(e, "#gas-station-latitude")
})

function isNumberKey(evt, selecter) {
    let charCode = (evt.which) ? evt.which : evt.keyCode;
    if (evt.ctrlKey) {

    } else {
        if (charCode > 47 && charCode < 58) {
            return
        } else if (charCode > 95 && charCode < 106) {
            return;
        } else if (charCode == 8) {
            return
        } else if (charCode == 190 || charCode == 110) {
            if ($(selecter).val().includes(".")) {
                evt.preventDefault();
            } else { return }
        } else {
            evt.preventDefault();
        }
    }
}
let customValidater = () => {
    let name = $("#gas-station-name").val();
    if (name.length > 0) {        
        if(name.length>100){
            console.log(1,$("#validator-for-gas-name"))
            $("#validator-for-gas-name").addClass("d-lg-flex").removeClass("d-none");
            $("#validator-for-gas-name-error").text("ガソリンスタンド名の長さは100文字を超えてはなりません");            
        }
        else {
            console.log(2,$("#validator-for-gas-name"))
            $("#validator-for-gas-name").addClass("d-none").removeClass("d-lg-flex");
        }
    } else {
        console.log(3,$("#validator-for-gas-name"))
        $("#validator-for-gas-name").removeClass("d-none").addClass("d-lg-flex");
        $("#validator-for-gas-name-error").text("ガソリンスタンド名を入力してください。");
    }
    let list = $("input[name='select-gas-type']:checked");
    if (list.length > 0) {
        $("#validator-for-gas-type").addClass("d-none").removeClass("d-lg-flex");
    } else {
        $("#validator-for-gas-type").removeClass("d-none").addClass("d-lg-flex");
    }
    let district = $("#select-district").val();
    if (district.length > 0) {
        $("#validator-for-gas-district").addClass("d-none").removeClass("d-lg-flex");
    } else {
        $("#validator-for-gas-district").removeClass("d-none").addClass("d-lg-flex");
    }
    let longitude = $("#gas-station-longitude").val();
    if (longitude.length > 0) {
        $("#validator-for-gas-longitude").addClass("d-none").removeClass("d-lg-flex");
    } else {
        $("#validator-for-gas-longitude").removeClass("d-none").addClass("d-lg-flex");
    }
    let latitude = $("#gas-station-latitude").val();    
    if (latitude.length > 0) {
        $("#validator-for-gas-latitude").addClass("d-none").removeClass("d-lg-flex");
    } else {
        $("#validator-for-gas-latitude").removeClass("d-none").addClass("d-lg-flex");
    }
    let latitude = $("#gas-station-latitude").val();    
    if (latitude.length > 0) {
        $("#validator-for-gas-latitude").addClass("d-none").removeClass("d-lg-flex");
    } else {
        $("#validator-for-gas-latitude").removeClass("d-none").addClass("d-lg-flex");
    }
}
$('#form-add-gas-station').change(() => {
    if (boot) {
        customValidater();
    }
});