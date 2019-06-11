# Xamarin.Forms 4 Certificate Pinning

This is a relatively complete example of certificate pinning in `Xamarin.Forms 4.0`. For now it only sends a request to `google.com` and checks its certificate.
On Android this requires some more work, but it's doable. You can send request using Refit or .NET.

For practice, you could refactor the code to connect to the included Azure Functions API project.
To check its certificate, use:

`openssl s_client -connect {YOUR DOMAIN}:443 | openssl x509 -pubkey -noout`

To write it to a file, use:

`echo -n | openssl s_client -connect google.com:443 | sed -ne '/-BEGIN CERTIFICATE-/,/-END CERTIFICATE-/p' > {YOUR_FILENAME}.cert`

## Sources used

<https://thomasbandt.com/certificate-and-public-key-pinning-with-xamarin>
<https://github.com/OWASP/CheatSheetSeries/blob/master/cheatsheets/Pinning_Cheat_Sheet.md>
<https://docs.microsoft.com/en-us/xamarin/android/app-fundamentals/http-stack?context=xamarin%2Fcross-platform>

## Troubleshooting

Android can still have some  name resolution failure issues. I wasn't able to resolve this.
