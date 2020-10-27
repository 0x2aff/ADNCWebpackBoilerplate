const path = require("path");

const CompressionPlugin = require("compression-webpack-plugin");
const MiniCssExtractPlugin = require("mini-css-extract-plugin");
const CopyPlugin = require("copy-webpack-plugin");
const OptimizeCssAssetsPlugin = require("optimize-css-assets-webpack-plugin");
const TerserPlugin = require("terser-webpack-plugin");


const { CleanWebpackPlugin } = require("clean-webpack-plugin");

module.exports = {
    devtool: "source-map",
    entry: {
        app: path.resolve(__dirname, "Client/Scripts/Index.js"),
    },
    output: {
        path: path.resolve(__dirname, "wwwroot/dist"),
        filename: "js/[name].js"
    },
    optimization: {
        minimize: true,
        minimizer: [
            new OptimizeCssAssetsPlugin(),
            new TerserPlugin()
        ],
        splitChunks: {
            cacheGroups: {
                vendors: {
                    test: /[\\/]node_modules[\\/]/,
                    name: "vendors",
                    chunks: "all"
                }
            }
        }
    },
    module: {
        rules: [
            {
                test: /\.css$/,
                use: [
                    MiniCssExtractPlugin.loader,
                    "css-loader"
                ]
            },
            {
                test: /\.scss$/,
                use: [
                    MiniCssExtractPlugin.loader,
                    "css-loader",
                    "sass-loader"
                ]
            },
            {
                test: /\.js$/,
                exclude: /[\\/]node_modules[\\/]/,
                use: {
                    loader: "babel-loader",
                    options: {
                        presets: ["@babel/preset-env"],
                        plugins: [
                            ["@babel/plugin-transform-runtime", { "regenerator": true }]
                        ]
                    }
                }
            },
            {
                test: /\.(png|svg|jpe?g|gif)(\??\#?v=[.0-9]+)?$/,
                loader: "file-loader",
                options: {
                    name: "img/[name].[ext]",
                    publicPath: "/dist"
                }
            },
            {
                test: /\.(eot|svg|ttf|woff|woff2|otf)(\??\#?v=[.0-9]+)?$/,
                loader: "file-loader",
                options: {
                    name: "fonts/[name].[ext]",
                    publicPath: "/dist"
                }
            }
        ]
    },
    plugins: [
        new CleanWebpackPlugin(),
        new CopyPlugin({
            patterns: [
                { from: "Client/Assets/Images", to: "img" }
            ]
        }),
        new CompressionPlugin(),
        new MiniCssExtractPlugin({
            filename: "css/[name].css"
        })
    ],
    performance: {
        hints: false,
        maxAssetSize: 512000
    },
};