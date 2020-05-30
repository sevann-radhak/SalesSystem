
class Principal {
    userLink(currentURL) {
        let url = "";
        let string = currentURL.split("/");

        for (var i = 0; i < string.length; i++) {
            if (string[i] != "Index") {
                url += string[i];
            }
        }

        switch (url) {
            case "UsersRegister":
                document.getElementById('files').addEventListener('change', imageUser, false);
                break;
        }
    }
}
