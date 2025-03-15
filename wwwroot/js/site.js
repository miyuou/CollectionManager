document.getElementById('viewNoteBtn').addEventListener('click', function () {
    const noteDisplay = document.getElementById('personalNoteDisplay');
    if (noteDisplay.style.display === 'none') {
        noteDisplay.style.display = 'block';
    } else {
        noteDisplay.style.display = 'none';
    }
});

// Attendre que le DOM soit prêt
$(document).ready(function () {
    // Script AJAX pour basculer l'état de favori sans rediriger
    $("form[id^='toggleForm-']").on("submit", function (e) {
        e.preventDefault(); // Empêcher la soumission normale du formulaire
        var formId = $(this).attr("id");
        var itemId = formId.split('-')[1]; // Extraire l'ID de l'élément

        $.ajax({
            url: '/Data/Index?handler=ToggleFavoriteWithId&id=' + itemId,  // Utiliser l'handler qui accepte un ID
            type: 'POST',
            success: function (response) {
                // Mettre à jour l'icône en fonction de l'état de favori
                var starButton = $("#" + formId + " button");
                if (starButton.text() == "☆") { // Si l'icône est une étoile vide
                    starButton.text("⭐"); // La rendre dorée
                    starButton.css("color", "#ffc107"); // Couleur dorée
                } else { // Si l'icône est déjà une étoile dorée
                    starButton.text("☆"); // La rendre vide
                    starButton.css("color", "#6c757d"); // Couleur grise
                }
            },
            error: function () {
                alert("Une erreur est survenue !");
            }
        });
    });
});
