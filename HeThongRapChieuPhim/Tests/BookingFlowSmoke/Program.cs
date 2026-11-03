using System.ComponentModel;
using System.ComponentModel.Design;
using System.Data;
using System.Reflection;
using HeThongRapChieuPhim.UserControls;
using HeThongRapChieuPhim.Classes;
internal static class Program {
 [STAThread] static void Main() {
  LicenseManager.CurrentContext = new DesigntimeLicenseContext();
  ApplicationConfiguration.Initialize();
  void Check(bool ok,string message) { if(!ok) throw new Exception(message); }
  TimeSpan H(string time)=>TimeSpan.Parse(time);
  Check(ShowtimeService.IsWithinOperatingHours(H("21:00"),120),"Midnight boundary should be valid");
  Check(!ShowtimeService.IsWithinOperatingHours(H("21:01"),120),"After midnight must be invalid");
  Check(!ShowtimeService.IsWithinOperatingHours(H("09:59"),120),"Opening boundary invalid");
  Check(!ShowtimeService.Overlaps(H("13:00"),H("16:00"),H("10:00"),H("13:00")),"Adjacent slots conflict");
  Check(ShowtimeService.Overlaps(H("12:59"),H("16:00"),H("10:00"),H("13:00")),"Cleaning overlap missed");
  using(var roster=new ucLichLamViec())
  using(var catalog=new ucChonPhim())
  using(var showPage=new ucBanHangShowtimes())
  using(var seatsPage=new ucBanHangSeats())
  using(var paymentPage=new ucBanHangPayment())
  using(var foodPage=new ucBanHangFood())
  using(var foodTemplate=new HeThongRapChieuPhim.UIHelpers.FoodItemCard())
  using(var movieTemplate=new HeThongRapChieuPhim.UIHelpers.MovieItemCard())
  using(var timeTemplate=new HeThongRapChieuPhim.UIHelpers.ShowtimeOption())
  using(var bookingForm=new HeThongRapChieuPhim.Forms.FormBooking())
  {
    Check(roster.Controls.Count>0 && catalog.Controls.Count>0 && showPage.Controls.Count>0 && seatsPage.Controls.Count>0 && paymentPage.Controls.Count>0,"Designer constructors failed");
    Check(foodTemplate.Item!=null && bookingForm.Booking!=null,"Default designer binding failed");
    var changeStep=typeof(ucBanHang).GetMethod("ShowBookingStep",BindingFlags.Instance|BindingFlags.NonPublic)!;
    changeStep.Invoke(bookingForm.Booking,new object[]{2});
    Check(bookingForm.ClientSize==new Size(600,710),"Compact payment window failed");
    changeStep.Invoke(bookingForm.Booking,new object[]{2});
    Check(bookingForm.ClientSize==new Size(600,710),"Compact food window failed");
    bookingForm.Show(); Application.DoEvents();
    var bookingType = typeof(ucBanHang);
    var foodFlow = (FlowLayoutPanel)bookingType.GetProperty("flpFoodItems", BindingFlags.Instance | BindingFlags.NonPublic)!.GetValue(bookingForm.Booking)!;
    var foodCards = (System.Collections.IList)bookingType.GetField("_foodCards", BindingFlags.Instance | BindingFlags.NonPublic)!.GetValue(bookingForm.Booking)!;
    for (int i = 0; i < 6; i++)
    {
      var foodCard = new HeThongRapChieuPhim.UIHelpers.FoodItemCard(new("F" + i, "Bắp rang bơ", "Mô tả", "", 35000m));
      foodFlow.Controls.Add(foodCard); foodCards.Add(foodCard);
    }
    bookingType.GetMethod("ResizeFoodCards", BindingFlags.Instance | BindingFlags.NonPublic)!.Invoke(bookingForm.Booking, null);
    foodFlow.AutoScrollPosition = new Point(900, 0);
    changeStep.Invoke(bookingForm.Booking,new object[]{1});
    changeStep.Invoke(bookingForm.Booking,new object[]{2}); Application.DoEvents();
    Check(foodFlow.AutoScrollPosition.X == 0 && !foodFlow.HorizontalScroll.Visible,"Food page retains horizontal scroll after shrinking");
    foreach (Control foodCard in foodFlow.Controls)
      Check(foodCard.Left >= 0 && foodCard.Right <= foodFlow.ClientSize.Width,"Food card clipped horizontally");
    changeStep.Invoke(bookingForm.Booking,new object[]{1});
    Check(bookingForm.ClientSize==new Size(1050,710),"Back to seats must restore wide window");
    changeStep.Invoke(bookingForm.Booking,new object[]{0});
    Check(bookingForm.ClientSize==new Size(1050,710),"Selection window width did not restore");
  }
  Console.WriteLine("PASS: all new Designer surfaces construct in design mode without database access.");
  using (var mainDesigner = new HeThongRapChieuPhim.Forms.FormMain())
  {
    object MainField(string name) => typeof(HeThongRapChieuPhim.Forms.FormMain).GetField(name, BindingFlags.Instance | BindingFlags.NonPublic)!.GetValue(mainDesigner)!;
    var toggle = (ToolStripItem)MainField("_sidebarToggle");
    var filmMenu = (ToolStripMenuItem)MainField("menuPhim");
    var original = (toggle.Text, toggle.Size, toggle.Padding, toggle.ForeColor, filmMenu.Text, filmMenu.Size, filmMenu.Font);
    var collapse = mainDesigner.GetType().GetMethod("SetSidebarCollapsed", BindingFlags.Instance | BindingFlags.NonPublic)!;
    collapse.Invoke(mainDesigner, new object[]{true}); collapse.Invoke(mainDesigner, new object[]{false});
    Check(original == (toggle.Text, toggle.Size, toggle.Padding, toggle.ForeColor, filmMenu.Text, filmMenu.Size, filmMenu.Font), "Sidebar must restore Designer text and appearance");
  }
  using (var template = new HeThongRapChieuPhim.UIHelpers.MovieItemCard())
  using (var copy = new HeThongRapChieuPhim.UIHelpers.MovieItemCard())
  {
    template.BackColor = Color.Beige; template.Size = new Size(320, 350);
    var templateBuy = template.Controls.OfType<Button>().Single();
    templateBuy.Text = "CHỌN PHIM"; templateBuy.BackColor = Color.Purple;
    copy.ApplyDesignTemplate(template);
    Check(copy.BackColor == Color.Beige && copy.Size == template.Size && copy.Controls.OfType<Button>().Single().Text == "CHỌN PHIM" && copy.Controls.OfType<Button>().Single().BackColor == Color.Purple, "Movie cards must inherit Designer template");
  }
  Console.WriteLine("PASS: Designer sidebar appearance survives collapse/expand; movie template settings copied.");
  using (var paymentLayout = new ucBanHangPayment())
  using (var paymentHost = new Form { ClientSize = new Size(580, 520) })
  {
    paymentHost.Controls.Add(paymentLayout); paymentLayout.Dock = DockStyle.Fill;
    paymentHost.Show(); Application.DoEvents();
    var totalLabel = (Label)typeof(ucBanHangPayment).GetField("lblSumTotal", BindingFlags.Instance | BindingFlags.NonPublic)!.GetValue(paymentLayout)!;
    totalLabel.Text = "TỔNG TIỀN:\r\n179,000 đ";
    foreach (float scale in new[] { 1f, 1.25f })
    {
      if (scale != 1f) paymentLayout.Scale(new SizeF(scale, scale));
      Application.DoEvents();
      int textHeight = TextRenderer.MeasureText(totalLabel.Text, totalLabel.Font, new Size(totalLabel.ClientSize.Width, int.MaxValue), TextFormatFlags.WordBreak).Height;
      Check(totalLabel.ClientSize.Height >= textHeight, "Total amount text is clipped");
      Check(totalLabel.Bottom <= totalLabel.Parent!.ClientSize.Height - totalLabel.Parent.Padding.Bottom, "Total amount extends outside bill panel");
    }
  }
  Console.WriteLine("PASS: payment total fits its label and bill panel at normal and enlarged layout sizes.");
  using (var strip = new ucChonPhim())
  using (var stripHost = new Form { ClientSize = new Size(1000, 720) })
  {
    object CatalogField(string name) => typeof(ucChonPhim).GetField(name, BindingFlags.Instance | BindingFlags.NonPublic)!.GetValue(strip)!;
    var movieStrip = (FlowLayoutPanel)CatalogField("_movies");
    var previous = (Button)CatalogField("previousMovies");
    var next = (Button)CatalogField("nextMovies");
    for (int i = 0; i < 6; i++) movieStrip.Controls.Add(new HeThongRapChieuPhim.UIHelpers.MovieItemCard());
    stripHost.Controls.Add(strip); strip.Dock = DockStyle.Fill; stripHost.Show(); Application.DoEvents();
    Check(movieStrip.WrapContents && movieStrip.Controls.Cast<Control>().Select(c => c.Top).Distinct().Count() == 2, "Movies must occupy two rows");
    Check(!previous.Enabled && next.Enabled, "Initial carousel arrows incorrect");
    next.PerformClick(); Application.DoEvents();
    Check(movieStrip.AutoScrollPosition.X < 0 && previous.Enabled, "Next arrow must scroll horizontally");
    for (int i = 0; i < 10; i++) next.PerformClick();
    Check(!next.Enabled, "Next arrow must stop at end");
    for (int i = 0; i < 10; i++) previous.PerformClick();
    Check(movieStrip.AutoScrollPosition.X == 0 && !previous.Enabled, "Previous arrow must return to start");
    stripHost.ClientSize = new Size(750, 720); Application.DoEvents();
    Check(movieStrip.Controls.Cast<Control>().Select(c => c.Top).Distinct().Count() == 2, "Resizing must retain two movie rows");
  }
  Console.WriteLine("PASS: two-row movie carousel, both navigation arrows, scroll boundaries and resizing.");
  using var view=new ucBanHang(); var t=view.GetType();
  object Field(string n)=>(t.GetField(n,BindingFlags.Instance|BindingFlags.NonPublic)?.GetValue(view) ?? t.GetProperty(n,BindingFlags.Instance|BindingFlags.NonPublic)?.GetValue(view))!;
  void Set(string n, object value)=>t.GetField(n,BindingFlags.Instance|BindingFlags.NonPublic)!.SetValue(view,value);
  var moviePicker=(ComboBox)Field("cboPhim");
  Check(moviePicker.Parent==null && ((Label)Field("lblSec1")).Parent==null,"Movie picker remains in booking layout");
  Set("_loadingMovies",true);
  var movies=new DataTable(); movies.Columns.Add("MaPhim"); movies.Columns.Add("TenPhim"); movies.Rows.Add("P1","Phim đã chọn");
  moviePicker.DisplayMember="TenPhim"; moviePicker.ValueMember="MaPhim"; moviePicker.DataSource=movies;
  Check(moviePicker.Items.Count==1 && moviePicker.SelectedItem is DataRowView,"External movie binding broken");
  Set("_loadingMovies",false);
  var combo=(ComboBox)Field("cboSuatChieu");
  var data=new DataTable();
  data.Columns.Add("MaSuatChieu"); data.Columns.Add("NgayChieu",typeof(DateTime)); data.Columns.Add("ShowTime"); data.Columns.Add("MaPhong"); data.Columns.Add("TenPhong");
  data.Rows.Add("S1",DateTime.Today,"17:20","P1","Phòng 1");
  data.Rows.Add("S2",DateTime.Today,"19:40","P1","Phòng 1");
  data.Rows.Add("S3",DateTime.Today.AddDays(1),"21:50","P1","Phòng 1");
  data.Rows.Add("S4",DateTime.Today,"20:30","P2","Phòng 2");
  Set("_bindingShowtimes",true);
  combo.DisplayMember="ShowTime"; combo.ValueMember="MaSuatChieu"; combo.DataSource=data;
  Set("_bindingShowtimes",false);
  t.GetMethod("RenderShowtimeOptions",BindingFlags.Instance|BindingFlags.NonPublic)!.Invoke(view,null);
  var dates=(ComboBox)Field("cboShowDate"); var options=(FlowLayoutPanel)Field("_showtimeOptions"); var rooms=(ComboBox)Field("cboShowRoom");
  Check(dates.Items.Count==2 && options.Controls.Count==3,"Date filter/all rooms failed");
  Check(combo.SelectedIndex==-1 && (string)Field("_currentMaSuatChieu")=="","Show auto selected");
  using var form=new Form { ClientSize=new Size(1500,850) }; form.Controls.Add(view); view.Dock=DockStyle.Fill; form.Show(); Application.DoEvents();
  var buttons=options.Controls.Cast<Control>().SelectMany(control=>control.Controls.OfType<Button>()).ToArray();
  Check(options.Controls[0].Top==options.Controls[1].Top && options.Controls[1].Left>options.Controls[0].Left,"Horizontal layout failed");
  // Avoid database access only for the explicit seat-load action; binding above used the real handler.
  combo.SelectedIndexChanged-=(EventHandler)Delegate.CreateDelegate(typeof(EventHandler),view,t.GetMethod("CboSuatChieu_SelectedIndexChanged",BindingFlags.Instance|BindingFlags.NonPublic)!);
  buttons[1].PerformClick(); Check(Convert.ToString(combo.SelectedValue)=="S2","Time click failed");
  Set("_currentMaSuatChieu","S2"); ((HashSet<string>)Field("_selectedSeats")).Add("A1");
  Check(rooms.Items.Count==3,"Room list failed"); rooms.SelectedIndex=2;
  Check(options.Controls.Count==1 && options.Controls[0].Controls.OfType<Button>().Single().Text=="20:30","Room filter failed");
  Check(combo.SelectedIndex==-1 && (string)Field("_currentMaSuatChieu")=="" && ((HashSet<string>)Field("_selectedSeats")).Count==0,"Stale show/seats after room change");
  dates.SelectedIndex=1;
  Check(rooms.Items.Count==2 && options.Controls.Count==1 && options.Controls[0].Controls.OfType<Button>().Single().Text.Contains("21:50"),"Day change failed");
  combo.DataSource=null; t.GetMethod("RenderShowtimeOptions",BindingFlags.Instance|BindingFlags.NonPublic)!.Invoke(view,null);
  Check(!dates.Enabled && !rooms.Enabled && combo.SelectedIndex==-1,"Empty movie retains options");
  Set("_currentGiaVe",80000m); Set("_currentMaSuatChieu","S1");
  var seatSet=(HashSet<string>)Field("_selectedSeats"); seatSet.Add("A1"); seatSet.Add("F1");
  var designerGrid = (DataGridView)Field("pnlSeatMap");
  designerGrid.GridColor = Color.Magenta; designerGrid.RowTemplate.Height = 46;
  designerGrid.Columns[0].MinimumWidth = 48;
  t.GetMethod("ConfigureSeatGrid",BindingFlags.Instance|BindingFlags.NonPublic)!.Invoke(view,null);
  Check(designerGrid.GridColor == Color.Magenta && designerGrid.RowTemplate.Height == 46 && designerGrid.Columns[0].MinimumWidth == 48, "Seat initialization overwrites Designer settings");
  var getPrice=t.GetMethod("GetRowTicketPrice",BindingFlags.Instance|BindingFlags.NonPublic)!;
  Check((decimal)getPrice.Invoke(view,new object[]{"A"})! ==80000m && (decimal)getPrice.Invoke(view,new object[]{"F"})! ==80000m,"Uniform ticket price failed");
  t.GetMethod("RenderSeatMap",BindingFlags.Instance|BindingFlags.NonPublic)!.Invoke(view,null);
  Check(Convert.ToString(((DataGridView)Field("pnlSeatMap")).Rows[0].HeaderCell.Value)=="A","Row price still shown");
  t.GetMethod("CalculateTotals",BindingFlags.Instance|BindingFlags.NonPublic)!.Invoke(view,null);
  Check(((Label)Field("lblSumTicketPrice")).Text.Contains(160000m.ToString("N0")),"Uniform ticket total failed");
  var combos=(ComboBox)Field("cboCombo");
  var deals=new DataTable(); deals.Columns.Add("MaCombo"); deals.Columns.Add("TenCombo"); deals.Columns.Add("SoLuongGhe",typeof(int)); deals.Columns.Add("PhanTramGiam",typeof(decimal));
  deals.Rows.Add("","Không áp dụng",0,0m); deals.Rows.Add("C2","Hai ghế",2,10m); deals.Rows.Add("C2B","Hai ghế tốt hơn",2,15m); deals.Rows.Add("C3","Ba ghế",3,20m);
  combos.DisplayMember="TenCombo"; combos.ValueMember="MaCombo"; combos.DataSource=deals;
  t.GetMethod("CalculateTotals",BindingFlags.Instance|BindingFlags.NonPublic)!.Invoke(view,null);
  Check(Convert.ToString(combos.SelectedValue)=="C2B" && (decimal)Field("_bookingTotal")==136000m,"Best matching combo/discount failed");
  var seatGrid=(DataGridView)Field("pnlSeatMap");
  Check(seatGrid.Rows[0].Cells[0].Style.BackColor==Color.FromArgb(22,163,74),"Selected seat must be green");
  var clickSeat=t.GetMethod("HandleSeatClick",BindingFlags.Instance|BindingFlags.NonPublic)!;
  clickSeat.Invoke(view,new object[]{"B1"});
  Check(seatSet.Count==3 && Convert.ToString(combos.SelectedValue)=="C3" && (decimal)Field("_bookingTotal")==192000m,"Adding seat must switch combo without blocking");
  clickSeat.Invoke(view,new object[]{"B1"}); clickSeat.Invoke(view,new object[]{"F1"});
  Check(seatSet.Count==1 && Convert.ToString(combos.SelectedValue)=="" && (decimal)Field("_bookingTotal")==80000m,"Removing seats must remove ineligible discount");
  combos.DataSource=null; Set("_requiredComboSeats",0); Set("_comboDiscountPct",0m);
  Console.WriteLine("PASS: green selected seats, best automatic combo, add/remove seats and discount recalculation.");
  seatSet.Clear(); Set("_currentMaSuatChieu",""); Set("_currentGiaVe",0m);
  Console.WriteLine("PASS: same show price for all seat rows, row labels without price, ticket total.");  var vip=new RoomVipSettings("C,D",20m);
  Check(vip.Price(80000m,"C")==96000m && vip.Price(80000m,"A")==80000m,"VIP surcharge failed");
  Check(new RoomVipSettings("C",0m).Price(80000m,"C")==80000m,"Zero surcharge failed");
  Check(new RoomVipSettings("C",12.5m).Price(80001m,"C")==90001m,"VIP rounding failed");
  Set("_vipSettings",vip); Set("_currentGiaVe",80000m); Set("_currentMaSuatChieu","S1");
  seatSet.Add("A1"); seatSet.Add("C1");
  t.GetMethod("CalculateTotals",BindingFlags.Instance|BindingFlags.NonPublic)!.Invoke(view,null);
  Check(((Label)Field("lblSumTicketPrice")).Text.Contains(176000m.ToString("N0")),"Mixed VIP/regular total failed");
  seatSet.Clear(); Set("_currentMaSuatChieu",""); Set("_currentGiaVe",0m); Set("_vipSettings",new RoomVipSettings("",0));
  using(var roomView=new ucPhongChieu())
  {
    form.Controls.Add(roomView); roomView.Dock=DockStyle.Fill; roomView.BringToFront(); Application.DoEvents();
    object RoomField(string name)=>roomView.GetType().GetField(name,BindingFlags.Instance|BindingFlags.NonPublic)!.GetValue(roomView)!;
    var vipRows=(CheckedListBox)RoomField("clbVipRows"); var pct=(NumericUpDown)RoomField("numVipPercent");
    Check(vipRows.Items.Count==6,"VIP row options mismatch");
    vipRows.SetItemChecked(2,true); pct.Value=20; Application.DoEvents();
    var preview=(DataGridView)RoomField("pnlSeatPreview");
    Check(preview.Rows[2].Cells[0].Style.BackColor==Color.FromArgb(255,229,153),"VIP preview color missing");
    ((NumericUpDown)RoomField("numSoHangGhe")).Value=2; Application.DoEvents();
    Check(vipRows.Items.Count==2 && vipRows.CheckedItems.Count==0,"Removed row remains VIP");
    Check(vipRows.Bottom<=vipRows.Parent!.ClientSize.Height && pct.Bottom<=pct.Parent!.ClientSize.Height,"VIP controls clipped");
  }
  Console.WriteLine("PASS: VIP rows, 0/20/12.5 percent pricing, rounding, mixed seats, preview and row resize.");  Set("_currentMaSuatChieu","S1");
  var advance=t.GetMethod("AdvanceBookingStep",BindingFlags.Instance|BindingFlags.NonPublic)!;
  advance.Invoke(view,null);
  Check((int)Field("_bookingStep")==1 && ((Panel)Field("pnlCenter")).Visible && !((Panel)Field("pnlRight")).Visible,"Wizard showtime to seats failed");
  seatSet.Add("A1"); advance.Invoke(view,null);
  Check((int)Field("_bookingStep")==2 && !((Panel)Field("pnlRight")).Visible && !((Panel)Field("pnlCenter")).Visible,"Wizard seats to food failed");
  Check(((FlowLayoutPanel)Field("flpFoodItems")).Visible,"Food page must be visible on its own step");
  advance.Invoke(view,null);
  Check((int)Field("_bookingStep")==3 && ((Panel)Field("pnlRight")).Visible,"Wizard food to payment failed");
  ((Button)Field("_stepBack")).PerformClick();
  Check((int)Field("_bookingStep")==2 && ((FlowLayoutPanel)Field("flpFoodItems")).Visible,"Payment back must return to food");
  ((Button)Field("_stepBack")).PerformClick();
  Check((int)Field("_bookingStep")==1 && seatSet.Contains("A1"),"Wizard back lost selected seats");
  ((Button)Field("_stepBack")).PerformClick();
  Check((int)Field("_bookingStep")==0 && ((Panel)Field("pnlLeft")).Visible,"Wizard back to shows failed");
  bool cancelled=false; view.BookingCancelled+=(_,_)=>cancelled=true;
  ((Button)Field("_stepBack")).PerformClick(); Check(cancelled,"Wizard return to catalog failed");
  seatSet.Clear(); Set("_currentMaSuatChieu","");
  Console.WriteLine("PASS: 4 wizard steps, separate food page, only one page visible, back retains seats, return to movie catalog.");  using var card=new HeThongRapChieuPhim.UIHelpers.FoodItemCard(new("F1","Coca Cola","Nước uống","",50000m));
  card.Location=new Point(0,0); card.Width=400; form.Controls.Add(card); card.BringToFront();
  card.QuantityChanged += (sender,args)=>t.GetMethod("FoodCard_QuantityChanged",BindingFlags.Instance|BindingFlags.NonPublic)!.Invoke(view,new object[] { sender!,args });
  Check(!card.Controls.OfType<ComboBox>().Any(),"Food card still has size selector");
  var add=card.Controls.OfType<Button>().Single(button=>button.Name=="_addButton");
  var remove=card.Controls.OfType<Button>().Single(button=>button.Text=="−");
  Check(!remove.Enabled,"Minus enabled with zero quantity");
  add.PerformClick(); add.PerformClick(); remove.PerformClick();
  Check(card.Controls.OfType<Label>().Any(label=>label.Text=="Đã chọn: 1"),"Food quantity mismatch");
  Check(((Label)Field("lblSumFood")).Text.Contains(50000m.ToString("N0")),"Food total must use item price");
  for(int i=0;i<25;i++) add.PerformClick();
  Check(!add.Enabled && card.Controls.OfType<Label>().Any(label=>label.Text=="Đã chọn: 20"),"Quantity maximum failed");
  t.GetMethod("ShowBookingStep",BindingFlags.Instance|BindingFlags.NonPublic)!.Invoke(view,new object[]{2});
  t.GetMethod("ShowBookingStep",BindingFlags.Instance|BindingFlags.NonPublic)!.Invoke(view,new object[]{1});
  t.GetMethod("ShowBookingStep",BindingFlags.Instance|BindingFlags.NonPublic)!.Invoke(view,new object[]{2});
  Check(card.Controls.OfType<Label>().Any(label=>label.Text=="Đã chọn: 20"),"Food quantity lost across steps");
  Console.WriteLine("PASS: food has no size selector; +/- quantities, item price, total and maximum.");  Console.WriteLine("PASS: opening/closing boundaries, cleaning conflicts, no automatic selection, dates, all rooms, room filter, horizontal layout, time click, reset seats, empty state.");
 }
}
