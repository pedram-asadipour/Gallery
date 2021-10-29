const parentDiv = $("#cards");

$(function() {

    const settings = {
        "url": "/api/gallery",
        "method": "GET",
        "timeout": 0,
        "headers": {
            "Content-Type": "application/json"
        },
        "error":function() {
            alert("Failed! Load Images");
        }
    };

    $.ajax(settings).done(function(response) {

        response.forEach(x => {

            const child = `
            <div class="card border-0">
                <img src="/img/${x.image}" alt="${x.name}" title="${x.name}" class="card-img" />
                <div class="card-img-overlay text-white">
                    <h5 class="card-title">Category : ${x.category}</h5>
                    <p class="card-text">Name : ${x.name}</div>
                    <a href="/img/${x.image}" download class="download text-white">⬇</a>
                </div>
            </div>
            `;

            parentDiv.prepend(child);

        });

    });
});


function UploadImage() {

    $("form").submit(function(e) {
        e.preventDefault();

        $(function () {
            $("#exampleModal").modal("toggle");
        });

        $.validator.setDefaults({ ignore: null });
        $.validator.unobtrusive.parse(this);

        const formData = new FormData(this);

        const settings = {
            "url": "/api/gallery",
            "method": "post",
            "enctype": "multipart/form-data",
            "dataType": "json",
            "processData": false,
            "contentType": false,
            "data": formData,
            "error": function () {
                alert("Failed! Upload Image");
            }
        };

        $.ajax(settings).done(function(response) {

            $("form").trigger("reset");

            const child = `
            <div class="card border-0">
                <img src="/img/${response.image}" alt="${response.name}" title="${response.name}" class="card-img" />
                <div class="card-img-overlay text-white">
                    <h5 class="card-title">Category : ${response.category}</h5>
                    <p class="card-text">Name : ${response.name}</div>
                    <a href="/img/${response.image}" download onclick="DownloadImage('${response.id}')" class="download text-white">⬇</a>
                </div>
            </div>
            `;

            parentDiv.prepend(child);

            alert("Success 🎂");

        });
    });
};