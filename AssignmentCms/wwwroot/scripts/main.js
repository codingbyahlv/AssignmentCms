document.addEventListener("DOMContentLoaded", function() {
    var buttons = document.querySelectorAll('a.btn[data-should-center]');

    buttons.forEach(function(button) {
        var shouldCenter = button.getAttribute('data-should-center') === 'True';
        var parentDiv = button.closest('div[data-content-element-type-alias="buttonLink"]');

        if (shouldCenter && parentDiv) {
            parentDiv.style.textAlign = 'center';
        } else if (parentDiv) {
            parentDiv.style.textAlign = 'left';
        }
    });
});

