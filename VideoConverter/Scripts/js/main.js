
$(document).ready(function () {
    $('#navbtn').on('click', function () {
        if ($("#navbar").hasClass("in")) {
            $(".mcon").animate({
                'padding-top': '0'
            });
            //$(".mcon").css("padding-top", "0");
        } else {
            //$(".mcon").css("padding-top", "10%");
            $(".mcon").animate({
                'padding-top': '20%'
            });
        }
        // Grab height of dropdown
        //var height = $('.dropdown').outerHeight();
    });

    $(document).keypress(function (event) {
        if (event.which === 13) {
            event.preventDefault();
            $("#getl").click();
        }
    });
    function foc() {
        var input = $('#url');
        var txt = input.val();
        input.focus(function () {
            $(this).val('');
        }).blur(function () {
            var el = $(this);
            if (el.val() === '')
                el.val(txt);
        });
    };


    $("#getl").on("click", function() {
        $('#res').empty();
        $('#tit').empty();
        $('#qrcode').empty();
        $('#im').attr("src", "");
        $("#resaultP").css("display", "none");
        $("#im").css("display", "none");
        $("#det").css("display", "none");
        $("#err").css("display", "none");
        $("#mp").css("display", "none");
        $("#load").css("display", "block");
        $("#dwnmp").css("display", "none");
        $("#dwn").css("display", "block");
        $("#description").css("display", "none");
        $('#getl').attr("disabled", "disabled");
        $("#auziodiv").css("display", "none");
        $("#t").val("");
        $("#hdxs").empty();

        //var ur = $("#url").val().trim();
        //if (ur.indexOf("https://") === -1 && ur.indexOf("http://") === -1 && ur.indexOf(".com") === -1 && ur.indexOf("www.") === -1) {
        //    $('#getl').removeAttr("disabled");
        //    $.ajax({
        //        type: "GET",
        //        url: "/Home/GetVid/?q=" + ur,
        //        datatype: "json",
        //        cache: false
        //    }).success(function(data) {

        //    }).error(function (xhr, ajaxOptions, thrownError) {
        //        $('#getl').removeAttr("disabled");
        //        var er = $("#err");
        //        er.html("<b>Error:</b> Please try again.");
        //        $("#load").css("display", "none");
        //        er.css("display", "block");
        //        er.addClass("alert alert-danger");
        //    });
        //}
        var lnk = $("#url").val().trim();
        if (lnk.indexOf("youtube.com") !== -1 || lnk.indexOf("instagram.com") !== -1 || lnk.indexOf("liveleak.com") !== -1 || lnk.indexOf("vine.co") !== -1 || lnk.indexOf("mixcloud.com") !== -1) {

            window.location.href = "/Home/Down?url=" + lnk;

        } else {

        


        var urld = escape($("#url").val().trim());

        $.ajax({
                type: "GET",
                url: "/api/Getlk/Get/?url=" + urld,
                datatype: "json",
                cache: false
            })
            .success(function(data) {
                $('#getl').removeAttr("disabled");
                if (data === null || typeof data === "undefined") {
                    var er = $("#err");
                    er.html("<b>Error:</b> Please check the URL and try again.");
                    $("#load").css("display", "none");
                    er.css("display", "block");
                    er.addClass("alert alert-danger");
                    return;
                }

                if (data.indexOf("/sc/") !== -1) {
                    var n = data.replace("/sc/", "");
                    $("#n").val(n);
                    $("#load").css("display", "none");
                    $("#mp").css("display", "none");
                    $("#auziodiv").css("display", "block");
                    $('#qrcode').qrcode({
                        width: 110,
                        height: 110,
                        text: "http://vidfrom.com/mp3/?n=" + n + "&t="
                    });
                    foc();
                    return;
                }


                if (data.indexOf("youtubemp3.io") === -1 && data.indexOf("twimg.com") === -1 && data.indexOf("akamaihd.net") === -1) {
                    var jo;
                    try {
                        jo = $.parseJSON(data);
                        if (typeof jo.error !== 'undefined' && !jQuery.isEmptyObject(jo.error) && jo.error.trim()) {
                            var errr = $("#err");
                            errr.html("<b>Error:</b> Please check the URL and try again.");
                            $("#load").css("display", "none");
                            errr.css("display", "block");
                            errr.addClass("alert alert-danger");
                            return;
                        }
                    } catch (e) {
                        var err = $("#err");
                        err.html("<b>Error:</b> Please check the URL and try again.");
                        $("#load").css("display", "none");
                        err.css("display", "block");
                        err.addClass("alert alert-danger");
                        return;
                    }
                    // facebook
                    if (typeof jo.NormalQ !== 'undefined' && jo.NormalQ.indexOf('fbcdn.net') !== -1) {

                        var dc = document;
                        //info
                        var fbinfOut = "";
                        var e = dc.getElementById("det");
                        e.innerHTML = "";
                        var fbinf = dc.createElement("div");
                        fbinf.setAttribute("class", "det");
                        fbinf.setAttribute("id", "indet");
                        if (jo.Image != undefined) fbinfOut += '<img class="img-responsive img-thumbnail im aim" style="height: 104px;width: 130px;float: left;max-width: 250px" src="' + jo.Image + '"  /></a>';
                        fbinfOut += '<br/><b>Title:</b>   <a href="#" class="lnk" id="lnktit" target="_blank" rel="nofollow">' + jo.Title + '</a><br />';

                        fbinfOut += '<br clear="both" /><br /><hr />';
                        fbinf.innerHTML = fbinfOut;
                        e.appendChild(fbinf);

                        // Download Links
                        var res = dc.getElementById("res");

                        var outf = "";
                        var nod = dc.createElement("div");

                        var outf2 = "";
                        var nod2 = dc.createElement("div");
                        //for HD
                        outf += '<input type="radio" data-dname="Download Video in HD" name="lk" id="1" checked ';
                        if (jo.HdQ != undefined) {
                            $('#dwn').attr('href', jo.HdQ);
                            outf += ' value="' + jo.HdQ + '"';
                        }

                        outf += '<label for="1">';
                        outf += ' Download Video in HD ';
                        outf += '</label>';
                        nod.innerHTML = outf;
                        res.appendChild(nod);

                        //for Normal Quality
                        outf2 += '<input type="radio" data-dname="Download Video in Normal Quality" name="lk" id="2" ';
                        if (jo.NormalQ != undefined) outf2 += ' value="' + jo.NormalQ + '"';

                        outf2 += '<label for="2">';
                        outf2 += ' Download Video in Normal Quality ';
                        outf2 += '</label>';
                        nod2.innerHTML = outf2;
                        res.appendChild(nod2);
                        $("#load").css("display", "none");
                        $("#resaultP").css("display", "block");
                        $("#im").css("display", "block");
                        $("#det").css("display", "block");
                        foc();
                        $("input[type='radio']").click(function() {
                            var href = $(this).val();
                            var n = $(this).attr("data-dname");
                            var d = $('#dwn');
                            d.attr("href", href);
                            d.attr("download", n);

                        });

                        $('#dwn').click(function() {
                            var hf = $(this).attr("href");
                            if (hf == null || hf == "" || hf == 'undefined') {
                                alert("Please select download format");
                                return false;
                            }
                            return true;
                        });

                        $("<style>#resaultP{width: 52%;background-color: #efefef;}#res{width: 100%;margin-left: 17%;}</style>").appendTo("head");

                        return;
                    }


                    if (typeof jo.imgsrc !== 'undefined' && typeof jo.links !== 'undefined' && jo.imgsrc.indexOf('vimeocdn.com') !== -1) {
                        // for viemo
                        //info
                        var d1 = document;
                        var outinf = "";
                        var e = d1.getElementById("det");
                        e.innerHTML = "";
                        var ndinf = d1.createElement("div");
                        ndinf.setAttribute("class", "det");
                        ndinf.setAttribute("id", "indet");
                        try {
                            jo.title = decodeURIComponent(escape(jo.title));
                        } catch (e) {
                            try {
                                jo.title = decodeURIComponent(unescape(jo.title));
                            } catch (e) {
                            }
                        }

                        if (jo.imgsrc != undefined) outinf += '<img class="img-responsive img-thumbnail im aim" style="height: 104px;width: 130px;float: left;max-width: 250px" src="' + jo.imgsrc + '"  /></a>';
                        outinf += '<br/><b>Title:</b>   <a href="#" class="lnk" id="lnktit" target="_blank" rel="nofollow">' + jo.title + '</a><br />';

                        outinf += '<br clear="both" /><br /> <hr style="margin-top: 7px;"/>';
                        ndinf.innerHTML = outinf;
                        e.appendChild(ndinf);
                        //------
                        $("<style>#res{margin-left: 34%;}</style>").appendTo("head");


                        var jid = 1;
                        for (key in jo.links) {
                            var output = "";
                            var nd = d1.createElement("div");

                            var ob = jo.links[key];
                            output += '<input type="radio" data-dname="' + jo.title + '" name="lk" id="' + jid + '"';
                            if (jid === 1) {
                                output += ' checked ';
                                if (ob.url != undefined) $('#dwn').attr('href', ob.url);
                            }
                            if (ob.url != undefined) output += ' value="' + ob.url + '"';
                            output += '<label for="' + jid + '">';
                            output += '  ' + ob.quality + '';

                            output += '</label>';

                            nd.innerHTML = output;
                            d1.getElementById("res").appendChild(nd);

                            jid++;
                        }
                        $("input[type='radio']").click(function() {
                            var href = $(this).val();
                            var n = $(this).attr("data-dname");
                            var d = $('#dwn');
                            d.attr("href", href);
                            d.attr("download", n);

                        });

                        $('#dwn').click(function() {
                            var hf = $(this).attr("href");
                            if (hf == null || hf == "" || hf == 'undefined') {
                                alert("Please select download format");
                                return false;
                            }
                            return true;
                        });
                        $("#load").css("display", "none");
                        $("#resaultP").css("display", "block");
                        $("#im").css("display", "block");
                        $("#det").css("display", "block");
                        var input1 = $('#url');
                        var txt1 = input1.val();
                        input1.focus(function() {
                            $(this).val('');
                        }).blur(function() {
                            var el = $(this);
                            if (el.val() === '')
                                el.val(txt1);
                        });


                        return;
                    }


                    if (typeof jo.info !== 'undefined' && typeof jo.info.domain !== 'undefined' && jo.info.domain.indexOf('soundcloud.com') === -1 && jo.info.domain.indexOf('mixcloud.com') === -1 && jo.info.domain.indexOf('vine.co') === -1 && jo.download_links !== null && jo.download_links !== undefined) {
                        var d = document;
                        var jaaid = 1;
                        var onlyBtn = false;

                        function popu(obj) {
                            if (obj === '' || obj === null || obj === "undefined") {
                                return;
                            }
                            if (typeof obj !== "undefined" && typeof obj.quality !== "undefined") {
                                if (obj.quality.indexOf('Subtitles') !== -1) {
                                    return;
                                }
                            }
                            if (obj.title != undefined)
                                try {
                                    obj.title = decodeURIComponent(escape(obj.title));
                                } catch (e) {
                                    try {
                                        obj.title = decodeURIComponent(unescape(obj.title));
                                    } catch (e) {
                                    }
                                }
                            var output = "";
                            var nd = d.createElement("div");
                            var cc = "";
                            if (typeof obj !== "undefined" && typeof obj.quality !== "undefined" && typeof obj.type !== "undefined" && obj.type !== "MP3") {

                                if (obj.quality.indexOf('Max') !== -1) {
                                    var q = obj.quality;
                                    q = q.replace("Max", "");
                                    q = q.replace("(", "");
                                    q = q.replace(")", "");
                                    obj.quality = q;
                                }
                            }

                            //if (obj.newtab == 1) cc += ' target="_blank"';

                            //if (obj.saveas == 1 && obj.noref == 1) {
                            //    if (navigator.userAgent.match(/webkit/i) != null) cc += ' rel="noreferrer"';
                            //    cc += ' onclick="return rd(this);"';
                            //} else if (obj.saveas != 1 && obj.noref == 1) {
                            //    if (navigator.userAgent.match(/webkit/i) != null) cc += ' rel="noreferrer"';
                            //    else cc += ' onclick="return rd(this);"';
                            //} else if (obj.saveas == 1) {
                            //    cc += ' onclick="return rs(this,\'' + escape(obj.title) + ' ' + obj.quality + '.' + obj.type.toLowerCase() + '\');"';
                            //}
                            function populateRadio() {
                                if (obj.url.indexOf('instagram.com/') !== -1) {

                                    if (obj.url != undefined) popBtn(obj.url, 'instagram MP4 ' + obj.quality);
                                    onlyBtn = true;


                                } else {
                                    output += '<input type="radio" data-dname="' + obj.title + '" name="lk" id="' + jaaid + '"';
                                    if (jaaid === 1) {
                                        output += ' checked ';
                                        if (obj.url != undefined) $('#dwn').attr('href', obj.url);
                                    }
                                    if (obj.url != undefined) output += ' value="' + obj.url + '"';
                                    output += '<label for="' + jaaid + '">';
                                    output += ' ' + obj.type + ' ' + obj.quality + '';
                                    if (!isNaN(obj.size) && obj.size != null) output += ' size : ' + (obj.size / 1024 / 1024).toFixed(1).toString() + ' MB';
                                    output += '</label>';
                                }

                            }

                            if (obj.url.indexOf('.googlevideo.com/') !== -1 && obj.saveas !== 1 && obj.type.indexOf('MP3') === -1) {
                                populateRadio();
                                $("#dwnmp").css("display", "block");
                            }
                            if (obj.url.indexOf('.googlevideo.com/') === -1 && obj.type.indexOf('MP3') === -1 && obj.type.indexOf('M4A') === -1) {
                                populateRadio();
                            }

                            nd.innerHTML = output;
                            d.getElementById("res").appendChild(nd);
                            jaaid++;
                        }

                        function popIn(obj) {

                            var output = "";
                            var e = d.getElementById("det");
                            e.innerHTML = "";
                            var nd = d.createElement("div");
                            nd.setAttribute("class", "det");
                            nd.setAttribute("id", "indet");
                            try {
                                obj.title = decodeURIComponent(escape(obj.title));
                            } catch (e) {
                                try {
                                    obj.title = decodeURIComponent(unescape(obj.title))
                                } catch (e) {
                                }
                            }

                            if (obj.image != undefined) output += '<a class="aim" href="' + obj.url + '" target="_blank" rel="nofollow"><img src="' + obj.image + '" class="im" style="max-width: 250px;" /></a>';
                            output += '<br/><b>Title: </b>   <a href="' + obj.url + '" class="lnk" id="lnktit" target="_blank" rel="nofollow">' + obj.title + '</a><br />';

                            if (obj.duration != undefined) output += '<br/><b>Duration: </b>  <span class="du">' + obj.duration + '</span>';
                            output += '<br clear="both" /><br /><hr style="margin-top: 5px;"/>';
                            nd.innerHTML = output;
                            e.appendChild(nd);
                        }

                        popIn(jo.info);


                        if (typeof jo.info !== 'undefined' && typeof jo.info.domain !== 'undefined' && jo.info.domain.indexOf('youtube.com') !== -1) {
                            var values = [];
                            var hq;
                            $.each(jo.download_links, function(index, value) {
                                if (value !== null && typeof value !== 'undefined' && typeof value.quality !== 'undefined') {
                                    if (value.quality !== "720p") {

                                        values[index] = value;
                                    } else {
                                        hq = value;
                                    }
                                }

                            });
                            if (typeof hq !== 'undefined' && hq != null) {
                                values.unshift(hq);
                            }

                            var web360;
                            if (typeof hq !== 'undefined' && hq != null) {
                                $.each(values, function(index, value) {

                                    if (value !== '' && value !== null && typeof value !== 'undefined' && typeof value.quality !== 'undefined') {
                                        if (value.quality.indexOf("360p") !== -1) {
                                            web360 = value;
                                            values[index] = '';
                                        }
                                    }

                                });
                            }
                            if (typeof web360 !== 'undefined' && web360 != null) {
                                values.splice(2, 0, web360);
                            }
                            for (key in values) {
                                if (typeof values !== 'undefined' && typeof values[key] !== 'undefined' && values[key] !== null) {
                                    popu(values[key]);
                                }

                            }
                            $("<style>#res{margin-left: 35%;}</style>").appendTo("head");

                        } else {
                            if (typeof jo.info !== 'undefined' && typeof jo.info.domain !== 'undefined' && jo.info.domain.indexOf('facebook.com') !== -1) {
                                for (key in jo.download_links) {
                                    if (jo.download_links[key].quality.indexOf('HD') !== -1) {
                                        jo.download_links[key].quality = "HD";
                                    }
                                    popu(jo.download_links[key]);
                                }
                            } else {
                                for (key in jo.download_links) {
                                    popu(jo.download_links[key]);
                                }
                            }

                        }


                        $("input[type='radio']").click(function() {
                            var href = $(this).val();
                            var n = $(this).attr("data-dname");
                            var d = $('#dwn');
                            d.attr("href", href);
                            d.attr("download", n);

                        });

                        $('#dwn').click(function() {
                            var hf = $(this).attr("href");
                            if (hf == null || hf == "" || hf == 'undefined') {
                                alert("Please select download format");
                                return false;
                            }
                            return true;
                        });
                        $("#load").css("display", "none");
                        $("#resaultP").css("display", "block");
                        $("#im").css("display", "block");
                        $("#det").css("display", "block");
                        if (onlyBtn) {
                            $("#resaultP").css("display", "none");
                            $("#dwn").css("display", "none");
                        }
                    }
                }

                if (data.indexOf("youtubemp3.io") !== -1 || data.indexOf("twimg.com") !== -1 || data.indexOf("akamaihd.net") !== -1) {

                    popBtn(data);

                } else {
                    // for sound cloud
                    try {
                        var obj = $.parseJSON(data);
                        if (obj !== null && typeof obj !== 'undefined' && typeof obj.info !== 'undefined' && typeof obj.info.domain !== 'undefined' && obj.info.domain.indexOf('soundcloud.com') !== -1) {
                            if (typeof obj.download_links[0] !== 'undefined' && typeof obj.download_links[0].url !== 'undefined') {

                                popBtn(obj.download_links[0].url);
                            }
                        } else if (obj !== null && typeof obj !== 'undefined' && typeof obj.info !== 'undefined' && typeof obj.info.domain !== 'undefined' && obj.info.domain.indexOf('mixcloud.com') !== -1) {
                            // for mixcloud.com
                            for (key in obj.download_links) {
                                if (obj.download_links[key].type.indexOf('MP3') !== -1) {
                                    //if (typeof obj.quality !== 'undefined') obj.quality = '';

                                    popBtn(obj.download_links[key].url);
                                }

                            }

                        } else if (obj !== null && typeof obj !== 'undefined' && typeof obj.info !== 'undefined' && typeof obj.info.domain !== 'undefined' && obj.info.domain.indexOf('vine.co') !== -1) {
                            // for vine.co
                            for (key in obj.download_links) {
                                if (obj.download_links[key].quality.indexOf('Original') !== -1) {
                                    //if (typeof obj.quality !== 'undefined') obj.quality = 'MP4 720P';

                                    popBtn(obj.download_links[key].url, 'MP4 720P');
                                }

                            }
                        }
                    } catch (e) {

                    }


                }

                function popBtn(data, q) {
                    var output = "";
                    var e = document.getElementById("mp");
                    var nd = document.createElement("div");
                    nd.setAttribute("style", "display: block;");
                    e.innerHTML = "";
                    if (data.indexOf("youtubemp3.io") !== -1 || data.indexOf("api.soundcloud.com") !== -1 || data.indexOf("mixcloud.com") !== -1) {
                        output += '<a class="btn btn-lg btn-success" href="' + data + '" download="1"> <span class="fa-stack" ><i class="fa fa-download fa-stack-1x"></i></span> Download MP3</a>';

                    } else if (data.indexOf("twimg.com") !== -1 || data.indexOf("akamaihd.net") !== -1) {
                        output += '<a class="btn btn-lg btn-success" href="' + data + '" download="1"> <span class="fa-stack" ><i class="fa fa-download fa-stack-1x"></i></span> Download Twitter MP4</a>';

                    } else if (data.indexOf('instagram.com/') !== -1 || data.indexOf("vine.co") !== -1) {
                        output += '<a class="btn btn-lg btn-success" href="' + data + '" download="1"> <span class="fa-stack" ><i class="fa fa-download fa-stack-1x"></i></span> Download ' + q + '</a>';

                    }
                    nd.innerHTML = output;
                    $("#load").css("display", "none");
                    e.appendChild(nd);
                    $("#mp").css("display", "block");
                }

                foc();

            })
            .error(function(xhr, ajaxOptions, thrownError) {
                //alert("error : " + xhr + ajaxOptions + thrownError);
                $('#getl').removeAttr("disabled");
                var er = $("#err");
                er.html("<b>Error:</b> Please check the URL and try again.");
                $("#load").css("display", "none");
                er.css("display", "block");
                er.addClass("alert alert-danger");
            });

    }


    });

   
    $("#dwnMp3").on("click", function () {
        //var u= $("input[type='radio']:checked").val();
        // var urld = escape(u);
        var urld = escape($("#url").val().trim());
        $.ajax({
            type: "GET",
            url: "/api/Getlk/GetMp3/?value=" + urld + "",
            datatype: "json",
            cache: false
        })
            .success(function (data) {
                var d = $('#dwn');
                d.attr("href", data);

                //var jo = $.parseJSON(data);
                //alert(jo);
            })
            .error(function (xhr, ajaxOptions, thrownError) {
                alert("error : " + xhr + ajaxOptions + thrownError);
            });


    });

  
});