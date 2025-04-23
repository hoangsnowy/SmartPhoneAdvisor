# 🚀 SMARTPHONE ADVISOR

**Hệ chuyên gia tư vấn chọn điện thoại thông minh theo nhu cầu**

---

## ✨ Tính năng nổi bật

- ✨ **Tư vấn chọn điện thoại theo nhu cầu cá nhân**
- ✨ **Suy diễn tri thức từ 100 luật gốc biểu diễn O-A-V**
- ✨ **Giao diện khảo sát trực quan, Bootstrap 5, Blazor WebAssembly**
- ✨ **Triển khai GitHub Pages, không cần backend server**
- ✨ **Cho phép chọn thuật toán suy diễn trực tiếp ngay trong giao diện**

---

## ⚙️ 3 Thuật toán suy diễn hỗ trợ

| Engine | Tên | Cách hoạt động |
|--------|------|-------------------------------|
| ✨ RuleEngine | Suy diễn theo luật (Forward Chaining + CF) | Match sự kiện với luật để suy ra gợi ý có độ tin cậy cao nhất |
| ✨ DecisionTree | Học máy (Decision Tree) | So sánh đầu vào với dữ liệu huấn luyện từ `training-data-from-rules.json` |
| ✨ BFSRuleEngine | Tìm kiếm theo đồ thị suy diễn | Duyệt BFS từ `Needs` tới kết luận từ `rule_graph-from-rules.json` |

---

## 🌐 Tổng quan giao diện

- **Trang /survey**: Form khảo sát người dùng (Giới tính, Độ tuổi, Ngân sách, Nhu cầu, Thương hiệu, Thuật toán)
- **Trang /result**: Hiển thị gợi ý tốt nhất kèm danh sách các luật match và diễn giải

---

## 🔄 Các file JSON quan trọng trong wwwroot

| Tên file | Mô tả |
|-----------|--------|
| `rules.json` | Danh sách 100 luật suy diễn gốc (RuleEngine dùng) |
| `training-data-from-rules.json` | Chuyển từ rules để huấn luyện DecisionTree |
| `rule_graph-from-rules.json` | Sinh graph từ luật cho duyệt BFS |

---

## 🚀 Cách deploy trên GitHub Pages

1. Push mã nguồn lên branch `main`
2. GitHub Actions sẽ publish thư mục `wwwroot` vào branch `gh-pages`
3. Truy cập website tại: `https://<your-username>.github.io/SmartPhoneAdvisor/`

---

## 📄 Technology stack
- Blazor WebAssembly (.NET 8)
- Bootstrap 5 UI
- GitHub Actions CI/CD
- JSON tri thức từ file

---

## 🌟 Tác giả: NGUYỄN MINH HOÀNG - HỆ CỔ SỔ TRI THỨC

