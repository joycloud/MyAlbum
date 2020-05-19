var fd = new FormData();
var array = [];

function dragoverHandler(evt) {
    evt.preventDefault();
}
function dropHandler(evt) {//evt 為 DragEvent 物件
    evt.preventDefault();
    let files = evt.dataTransfer.files;//由DataTransfer物件的files屬性取得檔案物件

    for (let i in files) {
        if (files[i].type == 'image/png' || files[i].type == 'image/jpeg') {

            let pics = {
                name: files[i].name,
                files: files[i]
            }
            array.push(pics);

            //將圖片在頁面預覽
            let fr = new FileReader();
            fr.onload = openfile(files[i]);
            fr.readAsDataURL;
            //新增上傳檔案，上傳後名稱為圖檔的陣列
            fd.append(files[i].name, files[i]);
            aa++;
        }
    }
}

var count = 0;
async function openfile(tr) {
    // await優先跑這段
    let img = await toBase64(tr);//將圖片轉成base64
    let imgx = document.createElement('img');
    imgx.style.margin = "10px";
    imgx.src = img;

    let photo = $(document.createElement('div')).attr({
        "class": 'photo-list',
        "id": 'list-' + count,
        "name": tr.name
    });

    $('#imgDIV').append(photo);
    photo.append('<div class="close" id="close-' + count + '"></div>');
    photo.append(imgx);
    photo.append('<div class="progress-label"></div> <div id="progress_bar"><div class="loading">0%</div></div>');
    count++;
}

// 抓file的內容
const toBase64 = file => new Promise((resolve, reject) => {
    const reader = new FileReader();
    reader.readAsDataURL(file);
    reader.onload = () => resolve(reader.result);
    reader.onerror = error => reject(error);
});


async function Upload() {
    let i = 0;
    for (var pair of fd.entries()) {
        i++;
    }

    if (i == 0) {
        alert('沒有上傳的照片!!');
    }
    else {
        // async/await 需搭配return使用，有回傳才往下跑
        let mes1 = await Albumname($('#filename').val());
        if (mes1.indexOf('Albumname Successed') == -1) {
            alert(mes1);
        }
        else {
            for (let pic of array) {
                let newfilename = mes1.replace('Albumname Successed;', '');
                console.log(newfilename);
                mes2 = await Pictures(newfilename, pic);
            }
            alert('Successed');
        }
    }
}

let Albumname = (filename) => {    
    let mes = '';
    $.ajax({
        type: "POST",
        // 預設true，是同步執行狀態，false非同步執行完成才往下跑
        async: false,
        url: "/Home/Albumname?filename=" + filename,
        processData: false,
        contentType: false,
        success: function (result) {
            mes = result;
        },
        error: function (xhr, status, error) {
            alert(xhr.responseText);
            mes = 'fales error!!';
        }
    });
    return mes;
};

let Pictures = (newfilename, pic) => {
    let fdd = new FormData();   
    fdd.append(pic.name, pic.files);

    console.log(pic);
    $.ajax({
        type: "POST",
        // 預設true，是同步執行狀態，false非同步執行完成才往下跑
        async: false,
        url: "/Home/Pictures?newfilename=" + newfilename,
        data: fdd,
        processData: false,
        contentType: false,
        success: function (result) {
            mes = result;
        },
        error: function (xhr, status, error) {
            alert(xhr.responseText);
            mes = 'fales error!!';
        }
    });
};

    //if (filename.length == 0)
    //    mes = '相簿名稱必填!!';
    //else if (fd.file == 0)
    //    mes = '沒有上傳的照片!!';
    //else {
    //    $.ajax({
    //        type: "POST",
    //        // 預設true，是同步執行狀態，false非同步執行完成才往下跑
    //        async: false,
    //        url: "/Home/Albumname?filename=" + filename,
    //        data: fd,
    //        processData: false,
    //        contentType: false,
    //        success: function (result) {
    //            mes = result;
    //        },
    //        error: function (xhr, status, error) {
    //            alert(xhr.responseText);
    //            mes = 'fales error!!';
    //        }
    //    });
    //}
    //return mes;

    //// Album insert/update
    //let Albumname = (filename) => {
    //    let mes = '';
    //    $.ajax({
    //        type: "POST",
    //        // 預設true，是同步執行狀態，false非同步執行完成才往下跑
    //        async: false,
    //        url: '/Home/Albumname',
    //        data: { filename: filename },
    //        success: function (result) {
    //            if (result == 'The filename has existed')
    //                mes = result;
    //        },
    //        error: function (xhr, status, error) {
    //            alert(xhr.responseText);
    //            mes = 'fales';
    //            //console.log("上傳失敗");
    //        }
    //    });
    //    return mes;
    //};

    //// files update
    //let files = () => {
    //    $.ajax({
    //        type: "POST",
    //        url: '/Home/UploadByAjax',
    //        contentType: false,
    //        processData: false,
    //        data: fd,
    //        success: function (result) {
    //            console.log(result);
    //        },
    //        error: function (xhr, status, error) {
    //            alert(xhr.responseText);
    //            //console.log("上傳失敗");
    //        }
    //    });
    //};