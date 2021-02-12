let elements = $("div[data-check]");
$.each(elements,function(v,k){
    $(k).find(".small-card:first").click();
});
