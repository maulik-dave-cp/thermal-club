const prj = require("./manifest.json");

// load all plugins in "devDependencies" into the variable $
const $ = require("gulp-load-plugins")({
    pattern: ["*"],
    scope: ["devDependencies"]
});

var gulp = require('gulp');
var merge = require('merge-stream');
var del = require('del');
var fs = require('fs');
var es = require('event-stream');
var watch = require('gulp-watch');

/**
 * List the available gulp tasks
 */
gulp.task('help', gulp.series([$.taskListing]));


/**
 * Front End
 */

// gulp.task('fonts', function() {
//     log("-> Coping fonts");

//     return gulp.src(prj.client.vendor.fonts.concat(prj.client.app.fonts))
//         .pipe($.changed(prj.client.dist.fonts))
//         .pipe(gulp.dest(prj.client.dist.fonts));
// });

// gulp.task('img', function() {
//     log("-> Coping images");

//     return gulp.src(prj.client.vendor.img.concat(prj.client.app.img))
//         .pipe($.changed(prj.client.dist.img))
//         .pipe(gulp.dest(prj.client.dist.img));
// });

// gulp.task("sass", () => {
//     log("-> Compiling sass");

//     return gulp.src(prj.client.app.sass)
//         .pipe($.plumber({
//             errorHandler: onError
//         }))
//         .pipe($.sourcemaps.init({
//             loadMaps: true
//         }))
//         .pipe($.sass({
//                 includePaths: prj.client.vendor.sass
//             })
//             .on("error", $.sass.logError))
//         .pipe($.cached("sass_compile"))
//         .pipe($.autoprefixer())
//         .pipe($.rename("app.css"))
//         .pipe($.sourcemaps.write("./"))
//         .pipe(gulp.dest(prj.client.dist.css));
// });

// gulp.task("css", () => {
//     log("-> Building client app css dev");

//     return gulp.src(prj.client.app.css)
//         .pipe($.plumber({
//             errorHandler: onError
//         }))
//         .pipe($.newer({
//             dest: prj.client.dist.css + 'style.css'
//         }))
//         .pipe($.sourcemaps.init({
//             loadMaps: true
//         }))
//         .pipe($.concat('style.css'))
//         .pipe($.cssnano({
//             discardComments: {
//                 removeAll: true
//             },
//             discardDuplicates: true,
//             discardEmpty: true,
//             minifyFontValues: true,
//             minifySelectors: true
//         }))
//         .pipe($.sourcemaps.write("./"))
//         .pipe(gulp.dest(prj.client.dist.css));
// });

// gulp.task("css:vendor", () => {
//     log("-> Building client vendor css");

//     return gulp.src(prj.client.vendor.css)
//         .pipe($.plumber({
//             errorHandler: onError
//         }))
//         .pipe($.newer({
//             dest: prj.client.dist.css + 'vendor.css'
//         }))
//         .pipe($.sourcemaps.init({
//             loadMaps: true
//         }))
//         .pipe($.concat('vendor.css'))
//         .pipe($.cssnano({
//             discardComments: {
//                 removeAll: true
//             },
//             discardDuplicates: true,
//             discardEmpty: true,
//             minifyFontValues: true,
//             minifySelectors: true
//         }))
//         .pipe($.sourcemaps.write("./"))
//         .pipe(gulp.dest(prj.client.dist.css));
// });

// gulp.task('js:vendor', function() {
//     log("-> Building client vendor scripts dev");

//     return gulp.src(prj.client.vendor.js)
//         .pipe($.sourcemaps.init({
//             loadMaps: true
//         }))
//         .pipe($.concat('vendor.js'))
//         .pipe($.sourcemaps.write('./'))
//         .pipe(gulp.dest(prj.client.dist.js));
// });

// gulp.task('js', function() {
//     log("-> Building client app scripts dev");

//     return gulp.src(prj.client.app.js.concat(prj.client.app.templates))
//         .pipe($.if(/html$/, buildTemplates()))
//         //.pipe($.jshint())
//         //.pipe($.jshint.reporter('default'))
//         .pipe($.sourcemaps.init())
//         .pipe($.concat('app.js'))
//         .pipe($.sourcemaps.write('./'))
//         .pipe(gulp.dest(prj.client.dist.js));
// });

// gulp.task('js:prod', function() {
//     return gulp.src(prj.client.vendor.js.concat(prj.client.app.js, prj.client.app.templates))
//         .pipe($.if(/html$/, buildTemplates()))
//         .pipe($.concat('app.js'))
//         //.pipe(plugins.ngAnnotate())
//         .pipe($.uglify())
//         .pipe($.size({
//             gzip: true,
//             showFiles: true
//         }))
//         .pipe(gulp.dest(prj.client.dist.js));
// });

// gulp.task('css:prod', function() {
//     var vendor = gulp.src(prj.client.vendor.css)
//         .pipe($.plumber({
//             errorHandler: onError
//         }));

//     var sass = gulp.src(prj.client.app.sass)
//         .pipe($.plumber({
//             errorHandler: onError
//         }))
//         .pipe($.sass({
//                 includePaths: prj.client.vendor.sass
//             })
//             .on("error", $.sass.logError))
//         .pipe($.autoprefixer());

//     var css = gulp.src(prj.client.app.css)
//         .pipe($.plumber({
//             errorHandler: onError
//         }));

//     return es.merge(vendor, sass, css)
//         .pipe($.concat('style.css'))
//         .pipe($.cssnano({
//             discardComments: {
//                 removeAll: true
//             },
//             discardDuplicates: true,
//             discardEmpty: true,
//             minifyFontValues: true,
//             minifySelectors: true
//         }))
//         .pipe($.size({
//             gzip: true,
//             showFiles: true
//         }))
//         .pipe(gulp.dest(prj.client.dist.css));
// });

/**
 * Back End
 */

gulp.task('js:admin:vendor', function() {
    log("-> Building admin vendor scripts dev");

    return gulp.src(prj.admin.vendor.js)
        .pipe($.sourcemaps.init({
            loadMaps: true
        }))
        .pipe($.concat('vendor.js'))
        .pipe($.sourcemaps.write('./'))
        .pipe(gulp.dest(prj.admin.dist.js));
});

gulp.task('js:admin:template', function() {
    return gulp.src(prj.admin.app.templates)
        .pipe($.if(/html$/, buildTemplates()))

    .pipe($.concat('template.js'))
        .pipe(gulp.dest(prj.admin.dist.js));
});

gulp.task('js:admin:app', function() {
    return gulp.src(prj.admin.app.js)
        // .pipe($.jshint())
        // .pipe($.jshint.reporter('default'))

    .pipe($.sourcemaps.init())
        .pipe($.concat('app.js'))
        .pipe($.sourcemaps.write('.'))
        .pipe(gulp.dest(prj.admin.dist.js));
});

gulp.task("sass:admin", () => {
    log("-> Compiling sass");

    var vendor = gulp.src(prj.admin.app.scss, {allowEmpty: true})
        .pipe($.plumber({
            errorHandler: onError
        }))
        .pipe($.sourcemaps.init({
            loadMaps: true
        }))
        .pipe($.sass({
                includePaths: prj.admin.vendor.scss
            })
            .on("error", $.sass.logError))
        .pipe($.cached("sass_compile"))
        .pipe($.autoprefixer());

    return es.merge(vendor)
        .pipe($.concat('app.css'))
        .pipe($.size({ gzip: true, showFiles: true }))
        .pipe($.sourcemaps.write("./"))
        .pipe(gulp.dest(prj.admin.dist.css));

    // return gulp.src(prj.admin.app.scss, {allowEmpty: true})
    //     .pipe($.sassGlob())
    //     .pipe($.plumber({
    //         errorHandler: onError
    //     }))
    //     .pipe($.sourcemaps.init({
    //         loadMaps: true
    //     }))
    //     .pipe($.sass({
    //             includePaths: prj.admin.vendor.scss
    //         })
    //         .on("error", $.sass.logError))
    //     .pipe($.cached("sass_compile"))
    //     .pipe($.autoprefixer())
    //     .pipe($.sourcemaps.write("./"))
    //     .pipe(gulp.dest(prj.admin.dist.css));
});

gulp.task("css:admin", () => {
    log("-> Building admin app css dev");

    return gulp.src(prj.admin.app.css)
        .pipe($.plumber({
            errorHandler: onError
        }))
        .pipe($.newer({
            dest: prj.admin.dist.css + 'app.css'
        }))
        .pipe($.sourcemaps.init({
            loadMaps: true
        }))
        .pipe($.concat('app.css'))
        .pipe($.cssnano({
            discardComments: {
                removeAll: true
            },
            discardDuplicates: true,
            discardEmpty: true,
            minifyFontValues: true,
            minifySelectors: true
        }))
        .pipe($.sourcemaps.write("./"))
        .pipe(gulp.dest(prj.admin.dist.css));
});

gulp.task("css:admin:vendor", () => {
    log("-> Building admin vendor css dev");

    return gulp.src(prj.admin.vendor.css)
        .pipe($.plumber({
            errorHandler: onError
        }))
        // .pipe($.newer({
        //     dest: prj.admin.dist.css + 'vendor.css'
        // }))
        .pipe($.sourcemaps.init({
            loadMaps: true
        }))
        .pipe($.concat('vendor.css'))
        // .pipe($.cssnano({
        //     discardComments: {
        //         removeAll: true
        //     },
        //     discardDuplicates: true,
        //     discardEmpty: true,
        //     minifyFontValues: true,
        //     minifySelectors: true
        // }))
        .pipe($.sourcemaps.write("./"))
        .pipe(gulp.dest(prj.admin.dist.css));
});

gulp.task('fonts:admin', function() {
    return gulp.src(prj.admin.vendor.fonts.concat(prj.admin.app.fonts))
        .pipe($.newer(prj.admin.dist.fonts))
        .pipe(gulp.dest(prj.admin.dist.fonts));
});

gulp.task('img:admin', function() {
    return gulp.src(prj.admin.vendor.images.concat(prj.admin.app.images))
        .pipe($.newer(prj.admin.dist.img))
        .pipe(gulp.dest(prj.admin.dist.img));
});

gulp.task('ckeditor:admin', function() {

    var ckeditorCodeMirror = gulp.src('./node_modules/ckeditor-codemirror-plugin/codemirror/**/*')
        .pipe($.newer(prj.admin.dist.js + '/ckeditor/codemirror'))
        .pipe(gulp.dest(prj.admin.dist.js + '/ckeditor/codemirror'));

    var ckStyleCopy = gulp.src('./AdminApp/assets/css/ckstyle.css')
        .pipe($.newer(prj.admin.dist.css + '/ckstyle.css'))
        .pipe(gulp.dest(prj.admin.dist.css));

    return merge(ckeditorCodeMirror, ckStyleCopy);
});

gulp.task('js:admin:prod', function() {
    return gulp.src(prj.admin.vendor.js.concat(prj.admin.app.js, prj.admin.app.templates))
        .pipe($.if(/html$/, buildTemplates()))
        .pipe($.concat('app.js'))
        //.pipe(plugins.ngAnnotate())
        //.pipe($.uglify())
        .pipe($.size({
            gzip: true,
            showFiles: true
        }))
        .pipe(gulp.dest(prj.admin.dist.js));
});

gulp.task('css:admin:prod', function() {
    var vendor = gulp.src(prj.admin.vendor.css)
        .pipe($.plumber({ errorHandler: onError }));

    var sass = gulp.src(prj.admin.app.sass)
        .pipe($.plumber({ errorHandler: onError }))
        .pipe($.sass({
                includePaths: prj.admin.vendor.sass
            })
            .on("error", $.sass.logError))
        .pipe($.autoprefixer());

    var css = gulp.src(prj.admin.app.css)
        .pipe($.plumber({ errorHandler: onError }));

    return es.merge(vendor, sass, css)
        .pipe($.concat('app.css'))
        .pipe($.cssnano({
            discardComments: {
                removeAll: true
            },
            discardDuplicates: true,
            discardEmpty: true,
            minifyFontValues: true,
            minifySelectors: true
        }))
        .pipe($.size({ gzip: true, showFiles: true }))
        .pipe(gulp.dest(prj.admin.dist.css));
});

/**
 * Common
 */

gulp.task('clean', function(cb) {
    return del(['./dist'], cb);
});

gulp.task('default', gulp.series([
    // 'fonts', 'img', 'sass', 'css', 'css:vendor', 'js', 'js:vendor',

    'css:admin:vendor',
    'sass:admin',
    // 'css:admin',
    'js:admin:vendor',
    'js:admin:template',
    'js:admin:app',
    'fonts:admin',
    'img:admin',

    // 'ckeditor:admin'
]));
gulp.task('build:prod', gulp.series([
    // 'fonts', 'img', 'css:prod', 'js:prod',
    'fonts:admin', 'img:admin', 'css:admin:prod', 'js:admin:prod', 'ckeditor:admin'
]));

gulp.task('watch', gulp.series(['default'], function() {
    // watch(prj.client.app.fonts, function() {
    //     gulp.task('fonts')();
    // });
    // watch(prj.client.app.img, function() {
    //     gulp.task('img')();
    // });
    // gulp.watch("./assets/sass/**/*.scss", gulp.series('sass'));
    // gulp.watch(prj.client.app.css, gulp.series('css'));
    // gulp.watch(prj.client.app.templates, gulp.series('js'));
    // gulp.watch(prj.client.app.js, gulp.series('js'));

    gulp.watch('./AdminApp/assets/scss/**/*.scss', gulp.series('sass:admin'));
    // gulp.watch(prj.admin.app.css, gulp.series('css:admin'));
    gulp.watch(prj.admin.app.js, gulp.series('js:admin:app'));
    gulp.watch(prj.admin.app.templates, gulp.series('js:admin:template'));
    gulp.watch(prj.admin.app.fonts, gulp.series('fonts:admin'));
    gulp.watch(prj.admin.app.images, gulp.series('img:admin'));
}));

// gulp.task('default', gulp.series([
//     'css:vendor',
//     'sass',
//     'css',
//     'js:vendor',
//     'js',
//     'fonts',
//     'img',

//     'css:admin:vendor',
//     'sass:admin',
//     'css:admin',
//     'js:admin:vendor',
//     'js:admin:template',
//     'js:admin:app',
//     'fonts:admin',
//     'img:admin',

//     'ckeditor:admin'
// ]));

// gulp.task('prod', gulp.series([
//     'js:prod',
//     'css:prod',
//     'fonts',
//     'img',

//     'js:admin:prod',
//     'css:admin:prod',
//     'fonts:admin',
//     'img:admin',
//     'ckeditor:admin'
// ]));

// gulp.task('watch', gulp.series(['default'], function () {

//     gulp.watch('./assets/sass/**/*.scss', gulp.series('sass'));
//     gulp.watch(client.app.css, gulp.series('css'));
//     gulp.watch(client.app.js, gulp.series('js'));
//     gulp.watch(client.app.templates, gulp.series('js'));
//     gulp.watch(client.app.fonts, gulp.series('fonts'));
//     gulp.watch(client.app.images, gulp.series('img'));

//     gulp.watch('./AdminApp/assets/sass/**/*.scss', gulp.series('sass:admin'));
//     gulp.watch(admin.app.css, gulp.series('css:admin'));
//     gulp.watch(admin.app.js, gulp.series('js:admin:app'));
//     gulp.watch(admin.app.templates, gulp.series('js:admin:template'));
//     gulp.watch(admin.app.fonts, gulp.series('fonts:admin'));
//     gulp.watch(admin.app.images, gulp.series('img:admin'));
// }));

const buildTemplates = function() {
    return es.pipeline(
        $.htmlmin({
            collapseWhitespace: true
        }),
        $.angularTemplatecache({
            module: 'myApp'
        })
    );
};

/**
 * Log a message or series of messages using chalk's blue color.
 * Can pass in a string, object or array.
 */
const log = (msg) => {
    $.fancyLog($.chalk.blue(msg));
};

const onError = (err) => {
    $.fancyLog($.chalk.red(err));
};