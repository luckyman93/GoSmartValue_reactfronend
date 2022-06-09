const puppeteer = require("puppeteer");

const options = {
    format: "A4",
    printBackground: true,
    left: "15px",
    top: "15px",
    right: "15px",
    bottom: "15px"
};

module.exports = async (callback, htmlContent) => {
    try {
        //launch headless chrome
        const browser = await puppeteer.launch({
            headless: true
        });
        // create empty browser page
        const page = await browser.newPage();
        //set screen size
        await page.emulateMedia("screen");
        // add content to page
        await page.setContent(htmlContent);
        //create pdf
        const pdf = await page.pdf(options);
        //close headless chrome browser
        await page.close();
        await browser.close();

        callback(null, pdf.readAsArrayBuffer);
        console.log("done - writing pdf to folder");
        return callback;

    } catch (e) {
        console.log(e);
    }
};

/* const htmlToPdf = require("html-pdf");
const jsreport = require('jsreport-core')();

module.exports = async (callback, htmlContent) => {
    try {

        jsreport.init().then(function() {
            return jsreport.render({
                template: {
                    content: htmlContent,
                    engine: 'jsrender',
                    recipe: 'phantom-pdf'
                },
                data: {
                    foo: "empty"
                }
            }).then(function(resp) {
                callback(null, resp.content.toJSON().data);
});
}).catch (function(e) {
    callback(e, null);

});

} catch (e) {
    console.log(e);
    jsreport.close();
}
};

*/
