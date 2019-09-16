var nmDataFormater = function (date) {
    return new Date(parseInt(date.substr(6))).toJSON().slice(0, 10);
}