# Taiwan No.1 Validation

提供兩個 `String` 的擴充方法：

1. 驗證台灣身分證字號
2. 驗證新舊式台灣外來人口統一證號（居留證號）

```csharp=

using TaiwanNo1.Validation;

// 台灣身分證字號
"A100000001".IsTwIdValid(); // Ture

// 新式台灣外來人口統一證號（居留證號）
"A800000005".IsTwIdValid(); // Ture
"A900000007".IsTwIdValid(); // Ture

// 新舊式台灣外來人口統一證號（居留證號）
"AA00000009".IsTwIdValid(true); // Ture
"AB00000001".IsTwIdValid(true); // Ture
"AC00000003".IsTwIdValid(true); // Ture
"AD00000005".IsTwIdValid(true); // Ture
```

Happy coding !! 😉
