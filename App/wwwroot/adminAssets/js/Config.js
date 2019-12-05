CKEDITOR.editorConfig = function (config) {
    // Define changes to default configuration here. For example:
    // config.uiColor = '#AADC6E';
    config.contentsLangDirection = 'rtl';
    config.language = 'fa';
    config.filebrowserImageUploadUrl = '/file-upload';
    config.toolbar = 'MyToolbar';
    config.toolbar_MyToolbar =
        [
            { name: 'document', items: ['Source', '-', 'Save', 'NewPage', 'DocProps', 'Preview', 'Print', '-', 'Templates'] },
            { name: 'clipboard', items: ['Cut', 'Copy', 'Paste', 'PasteText', 'PasteFromWord', '-', 'Undo', 'Redo'] },
            { name: 'editing', items: ['Find', 'Replace', '-', 'SelectAll', '-', 'SpellChecker', 'Scayt'] },
            { name: 'forms', items: ['Form', 'Checkbox', 'Radio', 'TextField', 'Textarea', 'Select', 'Button', 'ImageButton', 'HiddenField'] },
            { name: 'basicstyles', items: ['Bold', 'Italic', 'Underline', 'Strike', 'Subscript', 'Superscript', '-', 'RemoveFormat'] },
            { name: 'links', items: ['Link', 'Unlink', 'Anchor'] },
            { name: 'colors', items: ['TextColor', 'BGColor'] },
            '/',
            { name: 'insert', items: ['Image', 'Flash', 'Table', 'HorizontalRule', 'Smiley', 'SpecialChar', 'PageBreak', 'Iframe'] },
            { name: 'paragraph', items: ['NumberedList', 'BulletedList', '-', 'Outdent', 'Indent', '-', 'Blockquote', 'CreateDiv', '-', 'JustifyLeft', 'JustifyCenter', 'JustifyRight', 'JustifyBlock', '-', 'BidiLtr', 'BidiRtl'] },
            { name: 'styles', items: ['Styles', 'Format', 'Font', 'FontSize'] }

        ];
    config.stylesSet = [
        { name: 'پاراگراف راست چین', element: 'p', attributes: { 'class': 'pb-3 text-justify rtl' } },
        { name: 'پاراگراف وسط چین', element: 'p', attributes: { 'class': 'pb-3 text-center rtl' } },
        { name: 'تصویر وسط چین کامل', element: 'p', attributes: { 'class': 'text-center width-100 rtl' } },
        { name: 'عنوان 2 زنگ قالب راست چین', element: 'h2', attributes: { 'class': 'h5 font-weight-bold colo-theme py-2 rtl' } },
        { name: 'عنوان 3 زنگ قالب راست چین', element: 'h3', attributes: { 'class': 'h5 font-weight-bold colo-theme py-2 rtl' } },
        { name: 'عنوان 4 زنگ قالب راست چین', element: 'h4', attributes: { 'class': 'h5 font-weight-bold colo-theme py-2 rtl' } },
        { name: 'عنوان 5 زنگ قالب راست چین', element: 'h5', attributes: { 'class': 'h6 font-weight-bold colo-theme py-2 rtl' } },
        { name: 'عنوان 6 زنگ قالب راست چین', element: 'h6', attributes: { 'class': 'h6 font-weight-bold colo-theme py-2 rtl' } },
        { name: 'عنوان 2 تیره راست چین', element: 'h2', attributes: { 'class': 'h5 font-weight-bold text-grey py-2 rtl' } },
        { name: 'عنوان 3 تیره راست چین', element: 'h3', attributes: { 'class': 'h5 font-weight-bold text-grey py-2 rtl' } },
        { name: 'عنوان 4 تیره راست چین', element: 'h4', attributes: { 'class': 'h5 font-weight-bold text-grey py-2 rtl' } },
        { name: 'عنوان 5 تیره راست چین', element: 'h5', attributes: { 'class': 'h6 font-weight-bold text-grey py-2 rtl' } },
        { name: 'عنوان 6 تیره راست چین', element: 'h6', attributes: { 'class': 'h6 font-weight-bold text-grey py-2 rtl' } },
        { name: 'عنوان 2 رنگ قالب وسط چین', element: 'h2', attributes: { 'class': 'h5 font-weight-bold color-theme py-2 text-center rtl' } },
        { name: 'عنوان 3 رنگ قالب وسط چین', element: 'h3', attributes: { 'class': 'h5 font-weight-bold color-theme py-2 text-center rtl' } },
        { name: 'عنوان 4 رنگ قالب وسط چین', element: 'h4', attributes: { 'class': 'h5 font-weight-bold color-theme py-2 text-center rtl' } },
        { name: 'عنوان 5 رنگ قالب وسط چین', element: 'h5', attributes: { 'class': 'h6 font-weight-bold color-theme py-2 text-center rtl' } },
        { name: 'عنوان 6 رنگ قالب وسط چین', element: 'h6', attributes: { 'class': 'h6 font-weight-bold color-theme py-2 text-center rtl' } },
        { name: 'عنوان 2 تیره وسط چین', element: 'h2', attributes: { 'class': 'h5 font-weight-bold text-grey py-2 text-center rtl' } },
        { name: 'عنوان 3 تیره وسط چین', element: 'h3', attributes: { 'class': 'h5 font-weight-bold text-grey py-2 text-center rtl' } },
        { name: 'عنوان 4 تیره وسط چین', element: 'h4', attributes: { 'class': 'h5 font-weight-bold text-grey py-2 text-center rtl' } },
        { name: 'عنوان 5 تیره وسط چین', element: 'h5', attributes: { 'class': 'h6 font-weight-bold text-grey py-2 text-center rtl' } },
        { name: 'عنوان 6 تیره وسط چین', element: 'h6', attributes: { 'class': 'h6 font-weight-bold text-grey py-2 text-center rtl' } },
        { name: 'تصویر 100 درصد', element: 'img', attributes: { 'class': 'width-100 py-3' } },
        { name: 'تصویر 50 درصد', element: 'img', attributes: { 'class': 'width-50 py-3' } },
        { name: 'لیست', element: 'ul', attributes: { 'class': 'pr-5 py-4 mr-5 rtl' } }
    ];
};