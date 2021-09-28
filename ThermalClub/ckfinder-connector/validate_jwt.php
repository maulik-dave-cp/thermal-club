<?php
require 'bootstrap.php';
use Carbon\Carbon;

function validateJwt($jwt){

    try {
        if (!isset($jwt)) {
            return false;
        }
    
        // split the token
        $tokenParts = explode('.', $jwt);
        $header = base64_decode($tokenParts[0]);
        $payload = base64_decode($tokenParts[1]);
        $signatureProvided = $tokenParts[2];
    
        // check the expiration time - note this will cause an error if there is no 'exp' claim in the token
        $expiration = Carbon::createFromTimestamp(json_decode($payload)->exp);
        $tokenExpired = (Carbon::now()->diffInSeconds($expiration, false) < 0);
    
        // build a signature based on the header and payload using the secret
        $base64UrlHeader = base64UrlEncode($header);
        $base64UrlPayload = base64UrlEncode($payload);
        $secret = $_ENV['SECRET'];
        $signature = hash_hmac('sha256', $base64UrlHeader . "." . $base64UrlPayload, $secret, true);
        $base64UrlSignature = base64UrlEncode($signature);
    
        // verify it matches the signature provided in the token
        $signatureValid = ($base64UrlSignature === $signatureProvided);
    
        if ($tokenExpired)
            return false;
    
        if (!$signatureValid)
            return false;
    
        return true;
    }
    catch (exception $e) {
        return false;
    }
}