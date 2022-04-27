// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

var inicializeEmoji = () => {
    $(document).ready(function () {
        $("#emojiPicker").emojioneArea({
            filters: {
                flags: false,
                symbols: false,
                objects: false,
                smileys_people: false,
                activity: false,
                travel_places: false,
                tones: false
            },
            saveEmojisAs: 'image'
        });
    });

    $("#ClearEmojiArea").click(() => { document.querySelector('.emojionearea-editor').innerHTML = ''; });
}
